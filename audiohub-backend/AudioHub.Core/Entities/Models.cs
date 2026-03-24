using System.Collections.Generic;
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
        public string Id { get; set; } = "";

        [JsonPropertyName("title")]
        public string Title { get; set; } = "";

        [JsonPropertyName("artistsNames")]
        public string ArtistsNames { get; set; } = "";

        [JsonPropertyName("allArtistsNames")]
        public string AllArtistsNames { get; set; } = "";

        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; } = "";

        [JsonPropertyName("bigThumbnailUrl")]
        public string BigThumbnailUrl { get; set; } = "";

        [JsonPropertyName("duration")]
        public long Duration { get; set; }

        [JsonPropertyName("streamingStatus")]
        public int StreamingStatus { get; set; }

        [JsonPropertyName("isVIP")]
        public bool IsVIP { get; set; }

        [JsonPropertyName("link")]
        public string RelativeUrl { get; set; } = "";

        [JsonPropertyName("fullUrl")]
        public string FullUrl { get; set; } = "";

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
        public SongListData? SongList { get; set; }
    }

    public class SongListData
    {
        [JsonPropertyName("items")]
        public List<Song> Items { get; set; } = new();
    }

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
}
