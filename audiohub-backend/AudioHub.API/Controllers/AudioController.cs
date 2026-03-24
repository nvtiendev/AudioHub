using Microsoft.AspNetCore.Mvc;
using AudioHub.Core;
using AudioHub.Core.Entities;
using System;
using System.Threading.Tasks;

namespace AudioHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AudioController : ControllerBase
    {
        private readonly AudioClient _audioClient;

        public AudioController()
        {
            _audioClient = new AudioClient();
        }

        [HttpGet("song/{idOrUrl}")]
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

        [HttpGet("album/{idOrUrl}")]
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
    }
}
