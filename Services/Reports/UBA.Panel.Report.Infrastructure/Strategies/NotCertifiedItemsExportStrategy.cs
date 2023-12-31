using UBA.Panel.Report.Common.Enums;
using UBA.Panel.Report.Domain.Interfaces;

namespace UBA.Panel.Report.Infrastructure.Strategies;

public class NotCertifiedItemsExportStrategy : IExportStrategy
{
    private readonly IReportsRepository _repository;
    private readonly Guid _reportId;
    
    public NotCertifiedItemsExportStrategy(IReportsRepository repository, Guid reportId)
    {
        _repository = repository;
        _reportId = reportId;
    }
    
    public async Task<Domain.Data.AggregateRoots.Report> GetItemsToExportAsync()
    {
        return await _repository
            .GetReportWithDetailsForNotCertified(
                r => r.Id == _reportId,
                null);
    }
}