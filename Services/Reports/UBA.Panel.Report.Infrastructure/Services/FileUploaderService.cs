using Azure.Storage.Blobs;
using UBA.Panel.Report.Domain.Interfaces;

namespace UBA.Panel.Report.Infrastructure.Services;

public class FileUploaderService : IFileUploaderService
{
    private readonly BlobContainerClient _containerClient;

    public FileUploaderService(BlobContainerClient containerClient)
    {
        _containerClient = containerClient;
    }

    public async Task UploadFileToProcess(Guid reportId, string fileName, Stream file, Dictionary<string, string>? metadata = null)
    {
        BlobClient client = _containerClient.GetBlobClient($"{reportId}/{fileName}");
        await client.UploadAsync(file, metadata: metadata);
    }
}