using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VideoInstagram.DataLayer.Context;
using VideoInstagram.DataLayer.Entities;
using VideoInstagram.Shared.Contracts;

namespace VideoInstagram.DataLayer.Repositories
{
    public class VideoMetadataRepository : IVideoMetaDataRepository
    {
        private readonly IDataContextFactory _dataContextFactory;
        private readonly IMapper _mapper;

        public VideoMetadataRepository(IDataContextFactory dataContextFactory, IMapper mapper)
        {
            _dataContextFactory = dataContextFactory;
            _mapper = mapper;
        }

        public async Task CreateVideoMetadataAsync(VideoMetadata videoMetadata, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<VideoMetadataEntity>(videoMetadata);

            await using var dataContext = _dataContextFactory.GetDataContext();
            await dataContext.VideoMetadata.AddAsync(entity, cancellationToken).ConfigureAwait(false);

            await dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            videoMetadata.Id = entity.Id;
        }

        public async Task<VideoMetadata> GetVideoMetadataAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var dataContext = _dataContextFactory.GetDataContext();
            var VideoMetadata = await dataContext.VideoMetadata.FindAsync(id, cancellationToken).ConfigureAwait(false);
            return _mapper.Map<VideoMetadata>(VideoMetadata);
        }

        public async Task<IEnumerable<VideoMetadata>> GetAllVideoMetadatasAsync(double longitude, double latitude, CancellationToken cancellationToken = default)
        {
            await using var dataContext = _dataContextFactory.GetDataContext();
            var VideoMetadatas = await dataContext.VideoMetadata.Where(g => g.Latitude == latitude && g.Latitude == longitude).ToListAsync(cancellationToken).ConfigureAwait(false);
            return _mapper.Map<List<VideoMetadata>>(VideoMetadatas);
        }

        public async Task UpdateVideoMetadataAsync(VideoMetadata videoMetadata, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<VideoMetadataEntity>(videoMetadata);

            await using var dataContext = _dataContextFactory.GetDataContext();
            var toUpdate = await dataContext.VideoMetadata.FindAsync(entity.Id, cancellationToken).ConfigureAwait(false);

            if (toUpdate == null)
                return;

            _mapper.Map(entity, toUpdate);

            await dataContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }


        public async Task RemoveVideoMetadataAsync(int id, CancellationToken cancellationToken = default)
        {
            await using var dataContext = _dataContextFactory.GetDataContext();

            var VideoMetadata = await dataContext.VideoMetadata.FindAsync(id, cancellationToken).ConfigureAwait(false);

            dataContext.VideoMetadata.Remove(VideoMetadata);

            await dataContext
                .SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
