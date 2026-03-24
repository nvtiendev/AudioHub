using System;
using System.Threading;
using System.Threading.Tasks;
using AudioHub.Core.Entities;
using AudioHub.Core.Exceptions;
using AudioHub.Core.Utilities;

namespace AudioHub.Core.Clients
{
    public class PlaylistClient
    {
        readonly AudioClient _client;

        internal PlaylistClient(AudioClient client)
        {
            _client = client;
        }

        public async Task<Album> GetAsync(string idOrUrl, CancellationToken cancellationToken = default)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            // Ensure any URL-encoded characters (like slashes) are decoded before Regex matching
            idOrUrl = System.Net.WebUtility.UrlDecode(idOrUrl);
            
            string id = idOrUrl;
            var match = Regexes.PlaylistUrl.Match(idOrUrl);
            if (match.Success) id = match.Groups[2].Value;
=======
            string id = idOrUrl;
            if (Uri.TryCreate(idOrUrl, UriKind.Absolute, out _))
            {
                var match = Regexes.PlaylistUrl.Match(idOrUrl);
                if (match.Success) id = match.Groups[1].Value;
            }
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
=======
            // Ensure any URL-encoded characters (like slashes) are decoded before Regex matching
            idOrUrl = System.Net.WebUtility.UrlDecode(idOrUrl);
            
            string id = idOrUrl;
            var match = Regexes.PlaylistUrl.Match(idOrUrl);
            if (match.Success) id = match.Groups[2].Value;
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
            
            // Playlists and Albums use the same underlying get/info endpoint logic in this source
            return await _client.APIClient.GetAlbumAsync(id, cancellationToken);
        }
    }
}
