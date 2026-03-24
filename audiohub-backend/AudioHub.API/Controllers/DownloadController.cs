using Microsoft.AspNetCore.Mvc;
using AudioHub.Core;
using System.Net.Http;
using System.Net.Http.Headers;
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 0a1a414 (feat: Implement core audio client, API controllers for audio and downloads, and supporting utilities.)
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.IO.Compression;

namespace AudioHub.API.Controllers
{
    /// <summary>
    /// Cung cấp các phương thức tải nhạc về máy (MP3 cá lẻ hoặc ZIP cả album).
    /// </summary>
<<<<<<< HEAD
=======

namespace AudioHub.API.Controllers
{
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
=======
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
    [ApiController]
    [Route("api/[controller]")]
    public class DownloadController : ControllerBase
    {
        private readonly AudioClient _audioClient;
        private readonly HttpClient _httpClient;

<<<<<<< HEAD
<<<<<<< HEAD
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
=======
        public DownloadController()
=======
        public DownloadController(AudioClient audioClient, HttpClient httpClient)
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
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
<<<<<<< HEAD
                var song = await _audioClient.Songs.GetAsync(id);
                var streamUrl = await _audioClient.Songs.GetAudioStreamUrlAsync(id);
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
=======
                var song = await _audioClient.Songs.GetAsync(idOrUrl);
                var streamUrl = await _audioClient.Songs.GetAudioStreamUrlAsync(idOrUrl);
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
                
                var response = await _httpClient.GetAsync(streamUrl, HttpCompletionOption.ResponseHeadersRead);
                var stream = await response.Content.ReadAsStreamAsync();

<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
                var safeArtists = string.Join("_", song.AllArtistsNames.Split(Path.GetInvalidFileNameChars()));
                var safeTitle = string.Join("_", song.Title.Split(Path.GetInvalidFileNameChars()));
                var fileName = $"{safeArtists} - {safeTitle}.mp3";
                
                string encodedName = Uri.EscapeDataString(fileName);
                Response.Headers.Append("Content-Disposition", $"attachment; filename*=UTF-8''{encodedName}");
<<<<<<< HEAD
=======
                var fileName = $"{song.AllArtistsNames} - {song.Title}.mp3";
                Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{fileName}\"");
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
=======
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
                
                return File(stream, "audio/mpeg");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
        /// <summary>
        /// Tải về toàn bộ bài hát trong một Album/Playlist dưới dạng file nén ZIP.
        /// </summary>
        /// <param name="idOrUrl">ID hoặc URL của album/playlist từ ZingMP3.</param>
        /// <returns>File .zip chứa các bài hát không phải VIP.</returns>
        [HttpGet("album/{*idOrUrl}")]
        public async Task<IActionResult> DownloadAlbum(string idOrUrl)
<<<<<<< HEAD
        {
            try
            {
                var album = await _audioClient.Albums.GetAsync(idOrUrl);
=======
        [HttpGet("album/{id}")]
        public async Task<IActionResult> DownloadAlbum(string id)
        {
            try
            {
                var album = await _audioClient.Albums.GetAsync(id);
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
=======
        {
            try
            {
                var album = await _audioClient.Albums.GetAsync(idOrUrl);
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
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
<<<<<<< HEAD
<<<<<<< HEAD
                            var response = await _httpClient.GetAsync(streamUrl, HttpCompletionOption.ResponseHeadersRead);
                            var stream = await response.Content.ReadAsStreamAsync();
                            
                            var safeArtists = string.Join("_", song.AllArtistsNames.Split(Path.GetInvalidFileNameChars()));
                            var safeTitle = string.Join("_", song.Title.Split(Path.GetInvalidFileNameChars()));
                            var fileName = $"{safeArtists} - {safeTitle}.mp3";

                            var entry = archive.CreateEntry(fileName, CompressionLevel.NoCompression);
                            using (var entryStream = entry.Open())
                            {
                                await stream.CopyToAsync(entryStream);
=======
                            var response = await _httpClient.GetAsync(streamUrl);
                            var content = await response.Content.ReadAsByteArrayAsync();
=======
                            var response = await _httpClient.GetAsync(streamUrl, HttpCompletionOption.ResponseHeadersRead);
                            var stream = await response.Content.ReadAsStreamAsync();
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
                            
                            var safeArtists = string.Join("_", song.AllArtistsNames.Split(Path.GetInvalidFileNameChars()));
                            var safeTitle = string.Join("_", song.Title.Split(Path.GetInvalidFileNameChars()));
                            var fileName = $"{safeArtists} - {safeTitle}.mp3";

                            var entry = archive.CreateEntry(fileName, CompressionLevel.NoCompression);
                            using (var entryStream = entry.Open())
                            {
<<<<<<< HEAD
                                await entryStream.WriteAsync(content, 0, content.Length);
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
=======
                                await stream.CopyToAsync(entryStream);
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
                            }
                        }
                        catch { /* Skip failed songs */ }
                    }
                }
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
                
                string encodedAlbumName = Uri.EscapeDataString($"{album.Title}.zip");
                Response.Headers.Append("Content-Disposition", $"attachment; filename*=UTF-8''{encodedAlbumName}");

                return File(memoryStream, "application/zip");
<<<<<<< HEAD
=======
                return File(memoryStream, "application/zip", $"{album.Title}.zip");
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
=======
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
