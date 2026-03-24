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
    [ApiController]
    [Route("api/[controller]")]
    public class DownloadController : ControllerBase
    {
        private readonly AudioClient _audioClient;
        private readonly HttpClient _httpClient;

        public DownloadController()
        {
            _audioClient = new AudioClient();
            _httpClient = new HttpClient();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ProxyDownload(string id)
        {
            try
            {
                var song = await _audioClient.Songs.GetAsync(id);
                var streamUrl = await _audioClient.Songs.GetAudioStreamUrlAsync(id);
                
                var response = await _httpClient.GetAsync(streamUrl, HttpCompletionOption.ResponseHeadersRead);
                var stream = await response.Content.ReadAsStreamAsync();

                var fileName = $"{song.AllArtistsNames} - {song.Title}.mp3";
                Response.Headers.Add("Content-Disposition", $"attachment; filename=\"{fileName}\"");
                
                return File(stream, "audio/mpeg");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("album/{id}")]
        public async Task<IActionResult> DownloadAlbum(string id)
        {
            try
            {
                var album = await _audioClient.Albums.GetAsync(id);
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
                            var response = await _httpClient.GetAsync(streamUrl);
                            var content = await response.Content.ReadAsByteArrayAsync();
                            
                            var entry = archive.CreateEntry($"{song.AllArtistsNames} - {song.Title}.mp3");
                            using (var entryStream = entry.Open())
                            {
                                await entryStream.WriteAsync(content, 0, content.Length);
                            }
                        }
                        catch { /* Skip failed songs */ }
                    }
                }
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                return File(memoryStream, "application/zip", $"{album.Title}.zip");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
