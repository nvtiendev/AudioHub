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
<<<<<<< HEAD
<<<<<<< HEAD
            // Ensure any URL-encoded characters (like slashes) are decoded before Regex matching
            idOrUrl = System.Net.WebUtility.UrlDecode(idOrUrl);
            
            string id = idOrUrl;
            var match = Regexes.AlbumUrl.Match(idOrUrl);
            if (match.Success) id = match.Groups[2].Value;
            else if ((match = Regexes.ShortAlbumUrl.Match(idOrUrl)).Success) id = match.Groups[1].Value;
            else if ((match = Regexes.PlaylistUrl.Match(idOrUrl)).Success) id = match.Groups[2].Value;
            else if ((match = Regexes.PlaylistID.Match(idOrUrl)).Success) id = match.Value;
            
            if (!Regexes.AlbumID.IsMatch(id) && !Regexes.PlaylistID.IsMatch(id))
                throw new AudioHubException("Invalid Album/Playlist ID or URL");
            
            // Console.WriteLine($"[AudioHub API] Album ID Extracted: {id} from {idOrUrl}");
=======
            string id = idOrUrl;
            if (Uri.TryCreate(idOrUrl, UriKind.Absolute, out _))
            {
                var match = Regexes.AlbumUrl.Match(idOrUrl);
                if (match.Success) id = match.Groups[1].Value;
            }
            
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
=======
            // Ensure any URL-encoded characters (like slashes) are decoded before Regex matching
            idOrUrl = System.Net.WebUtility.UrlDecode(idOrUrl);
            
            string id = idOrUrl;
            var match = Regexes.AlbumUrl.Match(idOrUrl);
            if (match.Success) id = match.Groups[2].Value;
            else if ((match = Regexes.ShortAlbumUrl.Match(idOrUrl)).Success) id = match.Groups[1].Value;
            else if ((match = Regexes.PlaylistUrl.Match(idOrUrl)).Success) id = match.Groups[2].Value;
            else if ((match = Regexes.PlaylistID.Match(idOrUrl)).Success) id = match.Value;
            
            if (!Regexes.AlbumID.IsMatch(id) && !Regexes.PlaylistID.IsMatch(id))
                throw new AudioHubException("Invalid Album/Playlist ID or URL");
            
            // Console.WriteLine($"[AudioHub API] Album ID Extracted: {id} from {idOrUrl}");
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
            return await _client.APIClient.GetAlbumAsync(id, cancellationToken);
        }
    }
}
