using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace MyApp.Modules.Shared.Storage;

public class StorageService : IStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public StorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public byte[] Download(string directoryName, string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(directoryName);

        var blobClient = containerClient.GetBlobClient(fileName);

        var result = blobClient.DownloadContent();

        return result.Value.Content.ToArray();
    }

    public void Upload(string directoryName, IFormFile file)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(directoryName);

        using (var stream = file.OpenReadStream())
        {
            var _ = containerClient.UploadBlob(file.FileName, stream);
        }
    }
}
