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
            string id = idOrUrl;
            if (Uri.TryCreate(idOrUrl, UriKind.Absolute, out _))
            {
                var match = Regexes.PlaylistUrl.Match(idOrUrl);
                if (match.Success) id = match.Groups[1].Value;
            }
            
            // Playlists and Albums use the same underlying get/info endpoint logic in this source
            return await _client.APIClient.GetAlbumAsync(id, cancellationToken);
        }
    }
}
