using Moq;
using System.Linq.Expressions;
using UBA.Panel.Report.Domain.Data.ValueObjects;
using UBA.Panel.Report.Domain.Interfaces;
using UBA.Panel.Report.Infrastructure.Strategies;

namespace UBA.Panel.Report.Infrastructure.Tests.Strategies
{
    public class NotCertifiedItemsExportStrategyTests
    {
        private readonly Mock<IReportsRepository> _repositoryMock;

        public NotCertifiedItemsExportStrategyTests()
        {
            _repositoryMock = new Mock<IReportsRepository>();
        }

        [Fact]
        public async Task GetItemsToExportAsync_RepositoryThrowsException_ShouldThrowException()
        {
            // Arrange
            var reportId = Guid.NewGuid();
            var strategy = new NotCertifiedItemsExportStrategy(_repositoryMock.Object, reportId);
            _repositoryMock
                .Setup(x =>
                    x.GetReportWithDetailsForNotCertified(
                        It.IsAny<Expression<Func<Domain.Data.AggregateRoots.Report, bool>>>(),
                        It.IsAny<int?>()))
                .ThrowsAsync(new Exception());

            // Act, Assert
            await Assert.ThrowsAsync<Exception>(() => strategy.GetItemsToExportAsync());
        }

        [Fact]
        public async Task GetItemsToExportAsync_Successful_ShouldReturnReport()
        {
            // Arrange
            var report = new Domain.Data.AggregateRoots.Report("TestReport");
            var reportId = Guid.NewGuid();
            var strategy = new NotCertifiedItemsExportStrategy(_repositoryMock.Object, reportId);
            _repositoryMock
                .Setup(x =>
                    x.GetReportWithDetailsForNotCertified(
                        It.IsAny<Expression<Func<Domain.Data.AggregateRoots.Report, bool>>>(),
                        It.IsAny<int?>()))
                .ReturnsAsync(report);

            // Act
            var result = await strategy.GetItemsToExportAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Same(result, report);
        }
    }
}
