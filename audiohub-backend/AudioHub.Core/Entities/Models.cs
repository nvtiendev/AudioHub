<<<<<<< HEAD
using System.Collections.Generic;
=======
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
using System.Text.Json.Serialization;
using AudioHub.Core.Interfaces;
using AudioHub.Core.Utilities;

namespace AudioHub.Core.Entities
{
    public class Song : IAudioObject, IAudioSearchable
    {
        [JsonPropertyName("encodeId")]
        public string ID { get; set; } = "";

        [JsonPropertyName("id")]
<<<<<<< HEAD
        public string Id { get; set; } = "";
=======
        public string Id => ID;
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)

        [JsonPropertyName("title")]
        public string Title { get; set; } = "";

        [JsonPropertyName("artistsNames")]
<<<<<<< HEAD
        public string ArtistsNames { get; set; } = "";

        [JsonPropertyName("allArtistsNames")]
        public string AllArtistsNames { get; set; } = "";

        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; } = "";

        [JsonPropertyName("bigThumbnailUrl")]
=======
        public string AllArtistsNames { get; set; } = "";

        [JsonPropertyName("thumbnailM")]
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
        public string BigThumbnailUrl { get; set; } = "";

        [JsonPropertyName("duration")]
        public long Duration { get; set; }

        [JsonPropertyName("streamingStatus")]
        public int StreamingStatus { get; set; }

<<<<<<< HEAD
        [JsonPropertyName("isVIP")]
        public bool IsVIP { get; set; }
=======
        [JsonIgnore]
        public bool IsVIP => StreamingStatus != 0;
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)

        [JsonPropertyName("link")]
        public string RelativeUrl { get; set; } = "";

<<<<<<< HEAD
        [JsonPropertyName("fullUrl")]
        public string FullUrl { get; set; } = "";
=======
        [JsonIgnore]
        public string FullUrl => Constants.SOURCE_LINK.TrimEnd('/') + RelativeUrl;
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)

        [JsonPropertyName("album")]
        public Album? Album { get; set; }
    }

    public class PreviewInfo
    {
        [JsonPropertyName("startTime")]
        public int StartTime { get; set; }
        [JsonPropertyName("endTime")]
        public int EndTime { get; set; }
    }

    public class Album : IAudioObject, IAudioSearchable
    {
        [JsonPropertyName("encodeId")]
        public string ID { get; set; } = "";

        [JsonPropertyName("id")]
<<<<<<< HEAD
        public string Id { get; set; } = "";

        [JsonPropertyName("title")]
        public string Title { get; set; } = "";

        [JsonPropertyName("thumbnailM")]
        public string ThumbnailM { get; set; } = "";

        [JsonPropertyName("thumbnailUrl")]
        public string ThumbnailUrl { get; set; } = "";

        [JsonPropertyName("bigThumbnailUrl")]
        public string BigThumbnailUrl { get; set; } = "";

        [JsonPropertyName("song")]
        public SongListData? Song { get; set; }

        [JsonPropertyName("songList")]
=======
        public string Id => ID;

        [JsonPropertyName("title")]
        public string Title { get; set; } = "";
        [JsonPropertyName("thumbnailM")]
        public string ThumbnailUrl { get; set; } = "";
        [JsonPropertyName("song")]
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
        public SongListData? SongList { get; set; }
    }

    public class SongListData
    {
        [JsonPropertyName("items")]
        public List<Song> Items { get; set; } = new();
    }
<<<<<<< HEAD

    public class Lyric
    {
        [JsonPropertyName("sentences")]
        public List<Sentence> Sentences { get; set; } = new();
    }

    public class Sentence
    {
        [JsonPropertyName("words")]
        public List<Word> Words { get; set; } = new();
    }

    public class Word
    {
        [JsonPropertyName("startTime")]
        public int StartTime { get; set; }
        [JsonPropertyName("endTime")]
        public int EndTime { get; set; }
        [JsonPropertyName("data")]
        public string Content { get; set; } = "";
    }
=======
>>>>>>> 8c372e7 (Initialize professional full-stack AudioHub project)
}
