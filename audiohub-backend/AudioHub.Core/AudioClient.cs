using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AudioHub.Core.Clients;
using AudioHub.Core.Entities;
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
            Albums = new AlbumClient(this);
            Playlists = new PlaylistClient(this);
            Lyrics = new LyricClient(this);
        }

        public AudioClient(string apiKey = Constants.DEFAULT_API_KEY, string secret = Constants.DEFAULT_SECRET, string version = Constants.DEFAULT_VERSION) : this(new HttpClient(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.All,
        }), apiKey, secret, version)
        { }

        public SongClient Songs { get; }
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
        }
    }
}
