using Microsoft.AspNetCore.Mvc;
using AudioHub.Core;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.IO.Compression;

namespace AudioHub.API.Controllers
{
    /// <summary>
    /// Cung cấp các phương thức tải nhạc về máy (MP3 cá lẻ hoặc ZIP cả album).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DownloadController : ControllerBase
    {
        private readonly AudioClient _audioClient;
        private readonly HttpClient _httpClient;

        public DownloadController(AudioClient audioClient, HttpClient httpClient)
        {
            _audioClient = audioClient;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Tải về một bài hát cụ thể dưới dạng file MP3.
        /// </summary>
        /// <param name="idOrUrl">ID bài hát hoặc URL bài hát từ ZingMP3.</param>
        /// <returns>Dòng dữ liệu (stream) file MP3 kèm tên file đã được giải mã UTF-8.</returns>
        [HttpGet("{*idOrUrl}")]
        public async Task<IActionResult> ProxyDownload(string idOrUrl)
        {
            try
            {
                var song = await _audioClient.Songs.GetAsync(idOrUrl);
                var streamUrl = await _audioClient.Songs.GetAudioStreamUrlAsync(idOrUrl);
                
                var response = await _httpClient.GetAsync(streamUrl, HttpCompletionOption.ResponseHeadersRead);
                var stream = await response.Content.ReadAsStreamAsync();

                var safeArtists = string.Join("_", song.AllArtistsNames.Split(Path.GetInvalidFileNameChars()));
                var safeTitle = string.Join("_", song.Title.Split(Path.GetInvalidFileNameChars()));
                var fileName = $"{safeArtists} - {safeTitle}.mp3";
                
                string encodedName = Uri.EscapeDataString(fileName);
                Response.Headers.Append("Content-Disposition", $"attachment; filename*=UTF-8''{encodedName}");
                
                return File(stream, "audio/mpeg");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Tải về toàn bộ bài hát trong một Album/Playlist dưới dạng file nén ZIP.
        /// </summary>
        /// <param name="idOrUrl">ID hoặc URL của album/playlist từ ZingMP3.</param>
        /// <returns>File .zip chứa các bài hát không phải VIP.</returns>
        [HttpGet("album/{*idOrUrl}")]
        public async Task<IActionResult> DownloadAlbum(string idOrUrl)
        {
            try
            {
                var album = await _audioClient.Albums.GetAsync(idOrUrl);
                if (album.SongList == null || album.SongList.Items.Count == 0)
                    return BadRequest(new { message = "Album không có bài hát." });

                var songs = album.SongList.Items.Where(s => !s.IsVIP).ToList();
                if (songs.Count == 0)
                    return BadRequest(new { message = "Album này chỉ có bài VIP, không thể tải." });

                var memoryStream = new System.IO.MemoryStream();
                using (var archive = new System.IO.Compression.ZipArchive(memoryStream, System.IO.Compression.ZipArchiveMode.Create, true))
                {
                    foreach (var song in songs)
                    {
                        try
                        {
                            var streamUrl = await _audioClient.Songs.GetAudioStreamUrlAsync(song.ID);
                            var response = await _httpClient.GetAsync(streamUrl, HttpCompletionOption.ResponseHeadersRead);
                            var stream = await response.Content.ReadAsStreamAsync();
                            
                            var safeArtists = string.Join("_", song.AllArtistsNames.Split(Path.GetInvalidFileNameChars()));
                            var safeTitle = string.Join("_", song.Title.Split(Path.GetInvalidFileNameChars()));
                            var fileName = $"{safeArtists} - {safeTitle}.mp3";

                            var entry = archive.CreateEntry(fileName, CompressionLevel.NoCompression);
                            using (var entryStream = entry.Open())
                            {
                                await stream.CopyToAsync(entryStream);
                            }
                        }
                        catch { /* Skip failed songs */ }
                    }
                }
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                
                string encodedAlbumName = Uri.EscapeDataString($"{album.Title}.zip");
                Response.Headers.Append("Content-Disposition", $"attachment; filename*=UTF-8''{encodedAlbumName}");

                return File(memoryStream, "application/zip");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
