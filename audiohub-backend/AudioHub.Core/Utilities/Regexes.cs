using System.Text.RegularExpressions;

namespace AudioHub.Core.Utilities
{
    internal partial class Regexes
    {
<<<<<<< HEAD
<<<<<<< HEAD
        [GeneratedRegex("https://zmdjs.zmdcdn.me/zmp3-desktop/v(.*?)/static/js/main\\.min\\.js", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex MainJSRegex();
        internal static Regex MainMinJS => MainJSRegex();

        [GeneratedRegex(";var .=\"(.{32})\",.=\"(.{32})\",.=\\{publicKey:", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex APIKeySecretRegex();
        internal static Regex ApiKeySecret => APIKeySecretRegex();

        [GeneratedRegex(@"^[A-Z0-9]{8}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex IDRegex();
        internal static Regex AlbumID => IDRegex();

        [GeneratedRegex(@"zingmp3\.vn/album/(.*?)/([A-Z0-9]{8})\.html", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex AlbumUrlRegex();
        internal static Regex AlbumUrl => AlbumUrlRegex();
        
        [GeneratedRegex(@"zingmp3\.vn/album/([A-Z0-9]{8})\.html", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex ShortAlbumUrlRegex();
        internal static Regex ShortAlbumUrl => ShortAlbumUrlRegex();

        [GeneratedRegex(@"zingmp3\.vn/(?:nghe-si/)?(.*)", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex ArtistUrlRegex();
        internal static Regex ArtistUrl => ArtistUrlRegex();

        [GeneratedRegex(@"^I[A-Z0-9]{7}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex ArtistIDRegex();
        internal static Regex ArtistID => ArtistIDRegex();
        
        [GeneratedRegex(@"^[a-zA-Z0-9.-]*$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex ArtistAliasRegex();
        internal static Regex ArtistAlias => ArtistAliasRegex();

        [GeneratedRegex(@"^I[A-Z0-9]{7}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex GerneIDRegex();
        internal static Regex GenreID => GerneIDRegex();

        [GeneratedRegex(@"^[A-Z0-9]{8}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex PlaylistIDRegex();
        internal static Regex PlaylistID => PlaylistIDRegex();

        [GeneratedRegex(@"zingmp3\.vn/playlist/(.*?)/([A-Z0-9]{8})\.html", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex PlaylistUrlRegex();
        internal static Regex PlaylistUrl => PlaylistUrlRegex();

        [GeneratedRegex(@"zingmp3\.vn/bai-hat/(.*?)/(Z[A-Z0-9]{7})\.html", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex SongUrlRegex();
        internal static Regex SongUrl => SongUrlRegex();

        [GeneratedRegex(@"zingmp3\.vn/bai-hat/(Z[A-Z0-9]{7})\.html", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex ShortSongUrlRegex();
        internal static Regex ShortSongUrl => ShortSongUrlRegex();
        
        [GeneratedRegex(@"^Z[A-Z0-9]{7}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex SongIDRegex();
        internal static Regex SongID = SongIDRegex();

        [GeneratedRegex(@"zingmp3\.vn/video-clip/(.*?)/(Z[A-Z0-9]{7})\.html", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex VideoUrlRegex();
        internal static Regex VideoUrl => VideoUrlRegex();

        [GeneratedRegex(@"^Z[A-Z0-9]{7}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
=======
        [GeneratedRegex("https:\\/\\/zmdjs.zmdcdn.me\\/zmp3-desktop\\/v(.*?)\\/static\\/js\\/main\\.min\\.js", RegexOptions.Compiled)]
=======
        [GeneratedRegex("https://zmdjs.zmdcdn.me/zmp3-desktop/v(.*?)/static/js/main\\.min\\.js", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
        private static partial Regex MainJSRegex();
        internal static Regex MainMinJS => MainJSRegex();

        [GeneratedRegex(";var .=\"(.{32})\",.=\"(.{32})\",.=\\{publicKey:", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex APIKeySecretRegex();
        internal static Regex ApiKeySecret => APIKeySecretRegex();

        [GeneratedRegex(@"^[A-Z0-9]{8}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex IDRegex();
        internal static Regex AlbumID => IDRegex();

        [GeneratedRegex(@"zingmp3\.vn/album/(.*?)/([A-Z0-9]{8})\.html", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex AlbumUrlRegex();
        internal static Regex AlbumUrl => AlbumUrlRegex();
        
        [GeneratedRegex(@"zingmp3\.vn/album/([A-Z0-9]{8})\.html", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex ShortAlbumUrlRegex();
        internal static Regex ShortAlbumUrl => ShortAlbumUrlRegex();

        [GeneratedRegex(@"zingmp3\.vn/(?:nghe-si/)?(.*)", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex ArtistUrlRegex();
        internal static Regex ArtistUrl => ArtistUrlRegex();

        [GeneratedRegex(@"^I[A-Z0-9]{7}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex ArtistIDRegex();
        internal static Regex ArtistID => ArtistIDRegex();
        
        [GeneratedRegex(@"^[a-zA-Z0-9.-]*$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex ArtistAliasRegex();
        internal static Regex ArtistAlias => ArtistAliasRegex();

        [GeneratedRegex(@"^I[A-Z0-9]{7}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex GerneIDRegex();
        internal static Regex GenreID => GerneIDRegex();

        [GeneratedRegex(@"^[A-Z0-9]{8}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex PlaylistIDRegex();
        internal static Regex PlaylistID => PlaylistIDRegex();

        [GeneratedRegex(@"zingmp3\.vn/playlist/(.*?)/([A-Z0-9]{8})\.html", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex PlaylistUrlRegex();
        internal static Regex PlaylistUrl => PlaylistUrlRegex();

        [GeneratedRegex(@"zingmp3\.vn/bai-hat/(.*?)/(Z[A-Z0-9]{7})\.html", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex SongUrlRegex();
        internal static Regex SongUrl => SongUrlRegex();

        [GeneratedRegex(@"zingmp3\.vn/bai-hat/(Z[A-Z0-9]{7})\.html", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex ShortSongUrlRegex();
        internal static Regex ShortSongUrl => ShortSongUrlRegex();
        
        [GeneratedRegex(@"^Z[A-Z0-9]{7}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex SongIDRegex();
        internal static Regex SongID = SongIDRegex();

        [GeneratedRegex(@"zingmp3\.vn/video-clip/(.*?)/(Z[A-Z0-9]{7})\.html", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
        private static partial Regex VideoUrlRegex();
        internal static Regex VideoUrl => VideoUrlRegex();

<<<<<<< HEAD
        [GeneratedRegex(@"^Z[A-Z0-9]{7}$", RegexOptions.Compiled)]
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
=======
        [GeneratedRegex(@"^Z[A-Z0-9]{7}$", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
>>>>>>> 395a397 (feat: Implement initial AudioHub API backend for fetching and downloading audio data from ZingMP3.)
        private static partial Regex VideoIDRegex();
        internal static Regex VideoID => VideoIDRegex();
    }
}
