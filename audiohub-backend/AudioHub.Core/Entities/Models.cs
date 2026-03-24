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
        public string Id => ID;

        [JsonPropertyName("title")]
        public string Title { get; set; } = "";

        [JsonPropertyName("artistsNames")]
        public string AllArtistsNames { get; set; } = "";

        [JsonPropertyName("thumbnailM")]
        public string BigThumbnailUrl { get; set; } = "";

        [JsonPropertyName("duration")]
        public long Duration { get; set; }

        [JsonPropertyName("streamingStatus")]
        public int StreamingStatus { get; set; }

        [JsonIgnore]
        public bool IsVIP => StreamingStatus != 0;

        [JsonPropertyName("link")]
        public string RelativeUrl { get; set; } = "";

        [JsonIgnore]
        public string FullUrl => Constants.SOURCE_LINK.TrimEnd('/') + RelativeUrl;

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
        public string Id => ID;

        [JsonPropertyName("title")]
        public string Title { get; set; } = "";
        [JsonPropertyName("thumbnailM")]
        public string ThumbnailUrl { get; set; } = "";
        [JsonPropertyName("song")]
        public SongListData? SongList { get; set; }
    }

    public class SongListData
    {
        [JsonPropertyName("items")]
        public List<Song> Items { get; set; } = new();
    }
}
