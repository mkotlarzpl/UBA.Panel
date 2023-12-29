using UBA.Panel.Report.Domain.Data.ValueObjects;
using UBAReport = UBA.Panel.Report.Domain.Data.AggregateRoots.Report;

namespace UBA.Panel.Report.Domain.Interfaces;

public interface IReportsRepository
{
    public Task<UBAReport> CreateReportAsync(UBAReport report);
    public Task<UBAReport?> GetReportByNameAsync(string name);
    public Task<UBAReport?> GetReportByIdAsync(Guid id);
    public Task<UBAReport> UpdateReportAsync(UBAReport report);
}