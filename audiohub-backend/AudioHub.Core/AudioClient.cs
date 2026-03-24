using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AudioHub.Core.Clients;
<<<<<<< HEAD
<<<<<<< HEAD
using AudioHub.Core.Entities;
=======
using AudioHub.Core.Entities.Genres;
using AudioHub.Core.Entities.Songs;
using AudioHub.Core.Entities.Videos;
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
=======
using AudioHub.Core.Entities;
>>>>>>> 0a1a414 (feat: Implement core audio client, API controllers for audio and downloads, and supporting utilities.)
using AudioHub.Core.Exceptions;
using AudioHub.Core.Net;
using AudioHub.Core.Utilities;

namespace AudioHub.Core
{
    /// <summary>
    /// Client for interacting with the Audio Data Source.
    /// </summary>
    public class AudioClient
    {
        public AudioAPIClient APIClient { get; }
        public string APIKey { get; set; } = Constants.DEFAULT_API_KEY;
        public string Secret { get; set; } = Constants.DEFAULT_SECRET;
        public string Version { get; set; } = Constants.DEFAULT_VERSION;

        internal HttpClient HttpClient { get; }

        public AudioClient(HttpClient httpClient, string apiKey = Constants.DEFAULT_API_KEY, string secret = Constants.DEFAULT_SECRET, string version = Constants.DEFAULT_VERSION)
        {
            if (!httpClient.DefaultRequestHeaders.Contains("User-Agent"))
                httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(Utils.RandomUserAgent());
            HttpClient = httpClient;
            APIKey = apiKey;
            Secret = secret;
            Version = version;
            APIClient = new AudioAPIClient(this);
            Songs = new SongClient(this);
<<<<<<< HEAD
            Albums = new AlbumClient(this);
            Playlists = new PlaylistClient(this);
            Lyrics = new LyricClient(this);
=======
            Artists = new ArtistClient(this);
            Albums = new AlbumClient(this);
            Playlists = new PlaylistClient(this);
            Videos = new VideoClient(this);
            Genres = new GenreClient(this);
            Search = new SearchClient(this);
            CurrentUser = new UserClient(this);
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
        }

        public AudioClient(string apiKey = Constants.DEFAULT_API_KEY, string secret = Constants.DEFAULT_SECRET, string version = Constants.DEFAULT_VERSION) : this(new HttpClient(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.All,
        }), apiKey, secret, version)
        { }

        public SongClient Songs { get; }
<<<<<<< HEAD
        public AlbumClient Albums { get; }
        public PlaylistClient Playlists { get; }
        public LyricClient Lyrics { get; }

        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                Console.WriteLine($"[AudioHub API] Initializing from {Constants.SOURCE_LINK}...");
                var response = await HttpClient.GetAsync(Constants.SOURCE_LINK, cancellationToken);
                response.EnsureSuccessStatusCode();
                var html = await response.Content.ReadAsStringAsync(cancellationToken);

                // Strategy 1: Find all script URLs and check them
                var scriptMatches = Regex.Matches(html, "<script.*?src=\"(.*?)\"");
                foreach (Match sm in scriptMatches)
                {
                    var jsUrl = sm.Groups[1].Value;
                    if (jsUrl.Contains("main.") || jsUrl.Contains("vendor.") || jsUrl.Contains("desktop"))
                    {
                        if (!jsUrl.StartsWith("http")) jsUrl = Constants.SOURCE_LINK.TrimEnd('/') + jsUrl;
                        
                        try {
                            var jsResponse = await HttpClient.GetAsync(jsUrl, cancellationToken);
                            if (!jsResponse.IsSuccessStatusCode) continue;
                            var jsContent = await jsResponse.Content.ReadAsStringAsync(cancellationToken);
                            
                            var keyMatch = Regexes.ApiKeySecret.Match(jsContent);
                            if (keyMatch.Success)
                            {
                                APIKey = keyMatch.Groups[1].Value;
                                Secret = keyMatch.Groups[2].Value;
                                Console.WriteLine($"[AudioHub API] FOUND NEW KEYS! Key={APIKey.Substring(0, 5)}..., Secret={Secret.Substring(0, 3)}...");
                                return;
                            }
                        } catch { }
                    }
                }
                
                Console.WriteLine("[AudioHub API] WARNING: Could not rotate keys. API might reject requests with default keys.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[AudioHub API] ERROR: Initialization failed: {ex.Message}");
            }
=======
        public ArtistClient Artists { get; }
        public AlbumClient Albums { get; }
        public PlaylistClient Playlists { get; }
        public VideoClient Videos { get; }
        public GenreClient Genres { get; }
        public SearchClient Search { get; }
        public UserClient CurrentUser { get; }

        public async Task InitializeAsync(CancellationToken cancellationToken = default)
        {
            var httpResponse = await HttpClient.GetAsync(Constants.SOURCE_LINK, cancellationToken);
            if (!httpResponse.IsSuccessStatusCode)
                throw new AudioHubException($"Failed to fetch source page. Status code: {httpResponse.StatusCode}");
            
            string html = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            Match match = Regexes.MainMinJS.Match(html);
            Version = match.Groups[1].Value;
            string mainMinJSUrl = match.Value;
            
            var handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.All,
            };
            HttpClient client = new HttpClient(handler, true);
            client.DefaultRequestHeaders.Referrer = new Uri(Constants.SOURCE_LINK);
            httpResponse = await client.GetAsync(mainMinJSUrl, cancellationToken);
            if (!httpResponse.IsSuccessStatusCode)
                throw new AudioHubException($"Failed to fetch main.min.js. Status code: {httpResponse.StatusCode}");
            
            string mainMinJS = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
            int startIndex = mainMinJS.IndexOf("\"NON_LOGGED_ADD_RECENT_PLAYLIST\"");
            mainMinJS = mainMinJS.Substring(startIndex, mainMinJS.IndexOf("\"STORAGE_ADD_SONG\"") - startIndex);
            Match apiKeyAndSecretMatch = Regexes.ApiKeySecret.Match(mainMinJS);
            APIKey = apiKeyAndSecretMatch.Groups[1].Value;
            Secret = apiKeyAndSecretMatch.Groups[2].Value;
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
        }
    }
}
