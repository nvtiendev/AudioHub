using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AudioHub.Core.Exceptions;
using AudioHub.Core.Utilities;
using AudioHub.Core.Entities;

namespace AudioHub.Core.Clients
{
    public class SongClient
    {
        readonly AudioClient _client;

        internal SongClient(AudioClient client)
        {
            _client = client;
        }

        public async Task<Song> GetAsync(string idOrUrl, CancellationToken cancellationToken = default)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            // Ensure any URL-encoded characters (like slashes) are decoded before Regex matching
            idOrUrl = System.Net.WebUtility.UrlDecode(idOrUrl);
            
            string id = idOrUrl;
            var match = Regexes.SongUrl.Match(idOrUrl);
            if (match.Success) id = match.Groups[2].Value;
            else
            {
                match = Regexes.ShortSongUrl.Match(idOrUrl);
                if (match.Success) id = match.Groups[1].Value;
=======
=======
            // Ensure any URL-encoded characters (like slashes) are decoded before Regex matching
            idOrUrl = System.Net.WebUtility.UrlDecode(idOrUrl);
            
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
            string id = idOrUrl;
            var match = Regexes.SongUrl.Match(idOrUrl);
            if (match.Success) id = match.Groups[2].Value;
            else
            {
<<<<<<< HEAD
                var match = Regexes.SongUrl.Match(idOrUrl);
                if (match.Success) id = match.Groups[2].Value;
                else
                {
                    match = Regexes.ShortSongUrl.Match(idOrUrl);
                    if (match.Success) id = match.Groups[1].Value;
                }
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
=======
                match = Regexes.ShortSongUrl.Match(idOrUrl);
                if (match.Success) id = match.Groups[1].Value;
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
            }
            
            if (!Regexes.SongID.IsMatch(id))
                throw new AudioHubException("Invalid song ID/URL");
                
            return await _client.APIClient.GetSongAsync(id, cancellationToken);
        }

        public async Task<string> GetAudioStreamUrlAsync(string idOrUrl, AudioQuality quality = AudioQuality.Best, CancellationToken cancellationToken = default)
        {
<<<<<<< HEAD
<<<<<<< HEAD
            idOrUrl = System.Net.WebUtility.UrlDecode(idOrUrl);
            string id = idOrUrl;
            var match = Regexes.SongUrl.Match(idOrUrl);
            if (match.Success) id = match.Groups[2].Value;
            else
            {
                match = Regexes.ShortSongUrl.Match(idOrUrl);
                if (match.Success) id = match.Groups[1].Value;
            }
=======
            string id = idOrUrl;
            // ... (Simple ID extraction for now)
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
=======
            idOrUrl = System.Net.WebUtility.UrlDecode(idOrUrl);
            string id = idOrUrl;
            var match = Regexes.SongUrl.Match(idOrUrl);
            if (match.Success) id = match.Groups[2].Value;
            else
            {
                match = Regexes.ShortSongUrl.Match(idOrUrl);
                if (match.Success) id = match.Groups[1].Value;
            }
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
            return await _client.APIClient.GetAudioStreamUrlAsync(id, quality, cancellationToken);
        }
    }
}
