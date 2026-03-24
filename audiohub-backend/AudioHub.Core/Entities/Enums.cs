namespace AudioHub.Core.Entities
{
    public enum SubscriptionType
    {
        Free = 0,
        Plus = 1,
        Premium = 2
    }

    public enum SortType
    {
        Popular,
        Newest
    }

    public enum SearchFilter
    {
        Song,
        Artist,
        PlaylistAndAlbums,
        Video
    }

    public enum AudioQuality
    {
        Normal,
        High,
        Lossless,
        Best
    }
}
