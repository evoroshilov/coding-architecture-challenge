using System.Runtime.ExceptionServices;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace VideoInstagram.DataLayer.AzureBlobStorageService
{
    public class AzureBlobVideoStorageService
    {
        // here we can crud video actions with blob storage
        private readonly CloudBlobContainer _container;
        public async Task<Guid> UploadTemporaryVideoAsync(byte[] videoContent, long videoId)
        {
            try
            {
                var blob = _container.GetBlockBlobReference("from settings");

                blob.UploadFromByteArray(videoContent, 0, videoContent.Length);

            }
            catch (StorageException e) when (e.InnerException is OperationCanceledException operationCanceledException)
            {
                ExceptionDispatchInfo.Throw(operationCanceledException);
            }

            return await Task.FromResult(Guid.NewGuid());

        }

        //Update, Delete etc
    }
}
