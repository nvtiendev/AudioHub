using Microsoft.AspNetCore.Mvc;
using AudioHub.Core;
using AudioHub.Core.Entities;
using System;
using System.Threading.Tasks;

namespace AudioHub.API.Controllers
{
    /// <summary>
    /// Cung cấp các phương thức lấy thông tin âm nhạc từ ZingMP3.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AudioController : ControllerBase
    {
        private readonly AudioClient _audioClient;

        public AudioController(AudioClient audioClient)
        {
            _audioClient = audioClient;
        }

        /// <summary>
        /// Lấy thông tin chi tiết về một bài hát.
        /// </summary>
        /// <param name="idOrUrl">ID hoặc đường dẫn URL đầy đủ của bài hát.</param>
        /// <returns>Đối tượng Song chứa metadata bài hát.</returns>
        [HttpGet("song/{*idOrUrl}")]
        public async Task<IActionResult> GetSong(string idOrUrl)
        {
            try
            {
                var song = await _audioClient.Songs.GetAsync(idOrUrl);
                return Ok(song);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết về Album hoặc Playlist.
        /// </summary>
        /// <param name="idOrUrl">ID hoặc đường dẫn URL đầy đủ của album/playlist.</param>
        /// <returns>Đối tượng Album chứa danh sách bài hát.</returns>
        [HttpGet("album/{*idOrUrl}")]
        public async Task<IActionResult> GetAlbum(string idOrUrl)
        {
            try
            {
                var album = await _audioClient.Albums.GetAsync(idOrUrl);
                return Ok(album);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Lấy lời bài hát đồng bộ (có thông thời gian từng câu/từ).
        /// </summary>
        /// <param name="idOrUrl">ID bài hát (ví dụ: ZUCD7O78) hoặc link bài hát.</param>
        /// <returns>Đối tượng chứa danh sách câu lời nhạc.</returns>
        [HttpGet("lyric/{*idOrUrl}")]
        public async Task<IActionResult> GetLyrics(string idOrUrl)
        {
            try
            {
                var lyrics = await _audioClient.Lyrics.GetAsync(idOrUrl);
                return Ok(lyrics);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
