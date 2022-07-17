using VideoInstagram.Shared.Contracts;

namespace VideoInstagram.DataLayer.Repositories
{
    public interface IVideoMetaDataRepository
    {
        Task CreateVideoMetadataAsync(VideoMetadata videoMetadata, CancellationToken cancellationToken);
        Task<VideoMetadata> GetVideoMetadataAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<VideoMetadata>> GetAllVideoMetadatasAsync(double longitude, double latitude,
            CancellationToken cancellationToken = default);
        Task UpdateVideoMetadataAsync(VideoMetadata videoMetadata, CancellationToken cancellationToken);
        Task RemoveVideoMetadataAsync(int id, CancellationToken cancellationToken);
    }
}
