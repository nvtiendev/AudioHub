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
            // Ensure any URL-encoded characters (like slashes) are decoded before Regex matching
            idOrUrl = System.Net.WebUtility.UrlDecode(idOrUrl);
            
            string id = idOrUrl;
            var match = Regexes.PlaylistUrl.Match(idOrUrl);
            if (match.Success) id = match.Groups[2].Value;
            
            // Playlists and Albums use the same underlying get/info endpoint logic in this source
            return await _client.APIClient.GetAlbumAsync(id, cancellationToken);
        }
    }
}
