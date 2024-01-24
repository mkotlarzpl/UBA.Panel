using Azure.Storage.Blobs;
using Moq;
using UBA.Panel.Report.Domain.Interfaces;
using UBA.Panel.Report.Infrastructure.Services;

namespace UBA.Panel.Report.Infrastructure.Tests.Services
{
    public class FileUploaderServiceTests
    {
        private readonly Mock<BlobContainerClient> _blobContainerClientMock;
        private readonly IFileUploaderService _fileUploaderService;

        public FileUploaderServiceTests()
        {
            _blobContainerClientMock = new();
            _fileUploaderService = new FileUploaderService(_blobContainerClientMock.Object);
        }

        [Fact]
        public async Task UploadFileToProcess_UploadThrowsException_ShouldThrowException()
        {
            // Arrange
            var blobClientMock =  new Mock<BlobClient>();
            blobClientMock
                .Setup(x => x.UploadAsync(It.IsAny<Stream>(), null, It.IsAny<Dictionary<string, string>>(),
                    null, null, null, default, default))
                .ThrowsAsync(new Exception())
                .Verifiable();

            _blobContainerClientMock
                .Setup(x => x.GetBlobClient(It.IsAny<string>()))
                .Returns(blobClientMock.Object);

            // Act, Assert
            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _fileUploaderService.UploadFileToProcess(Guid.NewGuid(), "Test", Stream.Null);
            });
            _blobContainerClientMock.Verify();
        }

        [Fact]
        public async Task UploadFileToProcess_Upload_ShouldSucceeds()
        {
            // Arrange
            var blobClientMock = new Mock<BlobClient>();
            blobClientMock
                .Setup(x => x.UploadAsync(It.IsAny<Stream>(), null, It.IsAny<Dictionary<string, string>>(),
                    null, null, null, default, default))
                .Verifiable();

            _blobContainerClientMock
                .Setup(x => x.GetBlobClient(It.IsAny<string>()))
                .Returns(blobClientMock.Object);

            // Act, Assert
            await _fileUploaderService.UploadFileToProcess(Guid.NewGuid(), "Test", Stream.Null);
            _blobContainerClientMock.Verify();
        }
    }
}
