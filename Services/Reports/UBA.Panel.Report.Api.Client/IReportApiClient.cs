using UBA.Panel.Report.Common.DTOs;

namespace UBA.Panel.Report.Api.Client;

public interface IReportApiClient
{
    Task<HttpResponseMessage> CreateReportAsync(CreateReportDto createReportDto);
    Task<IEnumerable<ReportDto>> GetReports(int page);
    Task<ReportDetailsDto?> GetReportDetails(Guid reportId);
    Task<PagedResultDto<ReportItemDto>> GetReportItems(Guid report, int page);
    Task<PagedResultDto<ReportItemDto>> GetReportItemsWithDuplicatedVin(Guid report, int page);
    Task<PagedResultDto<ReportItemDto>> GetReportItemsElectricsForReport(Guid report, int page);
    Task<HttpResponseMessage> UpdateReportItemStatus(UpdateReportItemStatusDto updateReportItemStatusDto);
    Task<HttpResponseMessage> UploadFile(Guid reportId, string fileName, Stream fileStream);
    Task<HttpResponseMessage> DownloadReport(Guid reportId, string format);
}