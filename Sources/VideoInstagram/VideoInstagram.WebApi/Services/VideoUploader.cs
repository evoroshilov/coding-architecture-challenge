using VideoInstagram.DataLayer.AzureBlobStorageService;

namespace VideoInstagram.WebApi.Services
{
    public class VideoUploader
    {
        private readonly AzureBlobVideoStorageService _azureBlobVideoStorageService;

        public VideoUploader(AzureBlobVideoStorageService azureBlobVideoStorageService)
        {
            _azureBlobVideoStorageService = azureBlobVideoStorageService;
        }
        public Task<Guid> UploadVideoToTemporaryStorageAsync(long videoId, byte[] content)
        {
            return _azureBlobVideoStorageService.UploadTemporaryVideoAsync(content, videoId);
        }

    }
}
