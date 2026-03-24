using System;
using System.Threading;
using System.Threading.Tasks;
using AudioHub.Core.Entities;
using AudioHub.Core.Utilities;

namespace AudioHub.Core.Clients
{
    public class LyricClient
    {
        private readonly AudioClient _client;

        internal LyricClient(AudioClient client)
        {
            _client = client;
        }

        public async Task<Lyric> GetAsync(string idOrUrl, CancellationToken cancellationToken = default)
        {
            idOrUrl = System.Net.WebUtility.UrlDecode(idOrUrl);
            string id = idOrUrl;
            
            var match = Regexes.SongUrl.Match(idOrUrl);
            if (match.Success) id = match.Groups[2].Value;
            else
            {
                match = Regexes.ShortSongUrl.Match(idOrUrl);
                if (match.Success) id = match.Groups[1].Value;
            }

            return await _client.APIClient.GetLyricsAsync(id, cancellationToken);
        }
    }
}
