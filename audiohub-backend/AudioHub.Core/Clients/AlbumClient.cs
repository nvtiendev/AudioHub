using System;
using System.Threading;
using System.Threading.Tasks;
using AudioHub.Core.Entities;
using AudioHub.Core.Exceptions;
using AudioHub.Core.Utilities;

namespace AudioHub.Core.Clients
{
    public class AlbumClient
    {
        readonly AudioClient _client;

        internal AlbumClient(AudioClient client)
        {
            _client = client;
        }

        public async Task<Album> GetAsync(string idOrUrl, CancellationToken cancellationToken = default)
        {
            string id = idOrUrl;
            if (Uri.TryCreate(idOrUrl, UriKind.Absolute, out _))
            {
                var match = Regexes.AlbumUrl.Match(idOrUrl);
                if (match.Success) id = match.Groups[1].Value;
            }
            
            return await _client.APIClient.GetAlbumAsync(id, cancellationToken);
        }
    }
}
