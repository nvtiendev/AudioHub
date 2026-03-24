#pragma warning disable CS1591
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using AudioHub.Core.Entities;
using AudioHub.Core.Exceptions;
using AudioHub.Core.Interfaces;
using AudioHub.Core.Utilities;

namespace AudioHub.Core.Net
{
    public class AudioAPIClient
    {
        AudioClient _client;
        HttpClient http => _client.HttpClient;

        internal AudioAPIClient(AudioClient client)
        {
            _client = client;
        }

        // Simplified implementation to avoid huge file in one go, but keeping core logic
        async Task<JsonNode> SendGetAndCheckErrorCode(string path, Dictionary<string, object>? parameters, CancellationToken cancellationToken)
        {
            parameters ??= [];
            var pr = GetCommonParams();
            foreach (var param in parameters)
                pr.Add(param.Key, param.Value);
            pr.Add("sig", Sign(path, pr));
<<<<<<< HEAD
            
            string urlParam = Utils.ChainParams(pr);
            string fullUrl = Constants.SOURCE_LINK.TrimEnd('/') + Constants.API_BASE_PATH + path.TrimStart('/') + "?" + urlParam;
            
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            HttpResponseMessage response = await http.SendAsync(request, cancellationToken);
            
            string responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            Utils.CheckErrorCode(responseBody, out JsonNode result);
            
            if (!response.IsSuccessStatusCode)
                throw new AudioHubException($"API Error: {response.StatusCode}");
            
=======
            HttpRequestMessage request = CreateRequest(HttpMethod.Get, Constants.SOURCE_LINK.TrimEnd('/') + Constants.API_BASE_PATH + path.TrimStart('/'), pr);
            HttpResponseMessage response = await http.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();
            
            Utils.CheckErrorCode(await response.Content.ReadAsStringAsync(cancellationToken), out JsonNode result);
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
            return result;
        }

        Dictionary<string, object> GetCommonParams() => new Dictionary<string, object>()
        {
            { "ctime", DateTimeOffset.UtcNow.ToUnixTimeSeconds() },
            { "version", _client.Version },
            { "apiKey", _client.APIKey }
        };

        string Sign(string path, Dictionary<string, object> parameters)
        {
            var hashParams = parameters
                .Where(p => Constants.HASH_PARAMS.Contains(p.Key))
<<<<<<< HEAD
                .OrderBy(p => p.Key)
                .Select(p => $"{p.Key}={p.Value}")
                .ToList();

            string paramsString = string.Join("", hashParams);
            string sha256 = Utils.HashSHA256(paramsString);
            
            string signPath = Constants.API_BASE_PATH + path.TrimStart('/');
            string hash = signPath + sha256;
            return Utils.HashSHA512(hash, _client.Secret);
=======
                .Select(p => $"{p.Key}={p.Value}")
                .ToList();
            hashParams.Sort(string.Compare);
            string hash = Constants.API_BASE_PATH + path.TrimStart('/') + Utils.HashSHA256(string.Join("", hashParams));
            string sig = Utils.HashSHA512(hash, _client.Secret);
            return sig;
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
        }

        static HttpRequestMessage CreateRequest(HttpMethod method, string endpoint, Dictionary<string, object> parameters)
        {
            endpoint = endpoint.TrimEnd('/');
            string urlParam = Utils.ChainParams(parameters);
            HttpRequestMessage request = new HttpRequestMessage(method, endpoint + "?" + urlParam);
            return request;
        }

<<<<<<< HEAD
        // Bridge methods
        public async Task<Song> GetSongAsync(string id, CancellationToken cancellationToken = default)
        {
            string path = "page/get/song";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "id", id } };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var song = JsonSerializer.Deserialize<Song>(node.ToJsonString(), options) ?? throw new AudioHubException($"Cannot get song with id {id}.");
            
            // Manual mapping for frontend compatibility
            song.Id = song.ID;
            song.AllArtistsNames = song.ArtistsNames;
            song.BigThumbnailUrl = song.Thumbnail;
            song.IsVIP = song.StreamingStatus > 1; // 1 is Free, 2 is Plus, 3 is Premium
            song.FullUrl = Constants.SOURCE_LINK.TrimEnd('/') + song.RelativeUrl;
            
            return song;
=======
        // Bridge methods (Simplified for now, will expand as needed)
        public async Task<Song> GetSongAsync(string id, CancellationToken cancellationToken = default)
        {
            string path = "song/get/info";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "id", id } };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            return JsonSerializer.Deserialize<Song>(node.ToJsonString()) ?? throw new AudioHubException($"Cannot get song with id {id}.");
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
        }

        public async Task<Album> GetAlbumAsync(string id, CancellationToken cancellationToken = default)
        {
<<<<<<< HEAD
            string path = "page/get/playlist";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "id", id } };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var album = JsonSerializer.Deserialize<Album>(node.ToJsonString(), options) ?? throw new AudioHubException($"Cannot get album with id {id}.");
            
            // Manual mapping for frontend compatibility
            album.Id = album.ID;
            album.ThumbnailUrl = album.ThumbnailM;
            album.BigThumbnailUrl = album.ThumbnailM;
            album.SongList = album.Song;
            
            if (album.SongList?.Items != null)
            {
                foreach (var s in album.SongList.Items)
                {
                    s.Id = s.ID;
                    s.AllArtistsNames = s.ArtistsNames;
                    s.BigThumbnailUrl = s.Thumbnail;
                    s.IsVIP = s.StreamingStatus > 1; // 1 is Free, 2 is Plus, 3 is Premium
                    s.FullUrl = Constants.SOURCE_LINK.TrimEnd('/') + s.RelativeUrl;
                }
            }
            
            return album;
=======
            string path = "playlist/get/info"; // Albums are technically playlists in the API
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "id", id } };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            return JsonSerializer.Deserialize<Album>(node.ToJsonString()) ?? throw new AudioHubException($"Cannot get album with id {id}.");
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
        }

        public async Task<string> GetAudioStreamUrlAsync(string id, AudioQuality quality = AudioQuality.Best, CancellationToken cancellationToken = default)
        {
            string path = "song/get/streaming";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "id", id } };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
<<<<<<< HEAD
            
            string? url = node["128"]?.GetValue<string>();
            if (string.IsNullOrEmpty(url) || url == "VIP")
                throw new AudioHubException("No stream link available or it is a VIP song.");
                
            return url.StartsWith("//") ? "https:" + url : url;
        }

        public async Task<Lyric> GetLyricsAsync(string id, CancellationToken cancellationToken = default)
        {
            string path = "lyric/get/lyric";
            Dictionary<string, object> parameters = new Dictionary<string, object>() { { "id", id } };
            JsonNode node = await SendGetAndCheckErrorCode(path, parameters, cancellationToken);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<Lyric>(node.ToJsonString(), options) ?? throw new AudioHubException($"Cannot get lyrics for id {id}.");
=======
            // ... (Logic from ZingMP3APIClient.cs)
            return node["128"]?.GetValue<string>() ?? throw new AudioHubException("No stream link.");
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
        }
    }
}
