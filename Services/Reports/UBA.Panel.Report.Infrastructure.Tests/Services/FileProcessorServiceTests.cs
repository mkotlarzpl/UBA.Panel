using System.Linq.Expressions;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using Moq;
using UBA.Panel.Report.Domain.Interfaces;
using UBA.Panel.Report.Infrastructure.Services;

namespace UBA.Panel.Report.Infrastructure.Tests.Services
{
    public class FileProcessorServiceTests
    {
        private readonly Mock<IReportsRepository> _reportsRepositoryMock;
        private readonly Mock<BlobContainerClient> _sourceContainerClientMock;
        private readonly Mock<BlobContainerClient> _targetContainerClientMock;
        private readonly Mock<ILogger<FileProcessorService>> _loggerMock;
        private readonly Mock<IVinChecksumCalculator> _vinChecksumCalculatorMock;
        private readonly IFileProcessorService _fileProcessorService;

        public FileProcessorServiceTests()
        {
            _reportsRepositoryMock = new();
            _sourceContainerClientMock = new();
            _targetContainerClientMock = new();
            _loggerMock = new();
            _vinChecksumCalculatorMock = new();
            _fileProcessorService = new FileProcessorService(
                               _sourceContainerClientMock.Object,
                                              _targetContainerClientMock.Object,
                                              _reportsRepositoryMock.Object,
                                              _loggerMock.Object,
                                              _vinChecksumCalculatorMock.Object);
        }

        [Fact]
        public async Task Process_NotExistingReportId_ShouldThrowNullReferenceException()
        {
            // Arrange
            var stream = Stream.Null;

            // Act, Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await _fileProcessorService.Process(Guid.NewGuid(), "Test", stream);
            });
        }

        [Fact]
        public async Task Process_ReportExists_ShouldAppendReportItemAndMoveFiles()
        {
            // TODO: Finish test
            // Arrange
            var reportId = Guid.NewGuid();
            _reportsRepositoryMock
                .Setup(x => x.GetReportAsync(It.IsAny<Expression<Func<Domain.Data.AggregateRoots.Report, bool>>>()))
                .ReturnsAsync(new Domain.Data.AggregateRoots.Report("Test"));

            var stream = Stream.Null;

            // Act, Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () =>
            {
                await _fileProcessorService.Process(Guid.NewGuid(), "Test", stream);
            });
        }
    }
}
