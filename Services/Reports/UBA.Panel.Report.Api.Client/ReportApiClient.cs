using System.Net.Http.Json;
using UBA.Panel.Report.Api.Client.Config;
using UBA.Panel.Report.Common.DTOs;

namespace UBA.Panel.Report.Api.Client;

public class ReportApiClient : IReportApiClient
{
    private readonly HttpClient _httpClient;
    
    public ReportApiClient(ReportApiClientConfig config, IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(config.Endpoint);
    }
    
    public Task<HttpResponseMessage> CreateReportAsync(CreateReportDto createReportDto)
    {
        return _httpClient.PostAsJsonAsync("/api/reports", createReportDto);
    }

    public async Task<IEnumerable<ReportDto>> GetReports(int page)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<ReportDto>>($"/api/Reports/all/{page}") 
               ?? Array.Empty<ReportDto>();
    }

    public async Task<ReportDetailsDto?> GetReportDetails(Guid reportId)
    {
        return await _httpClient.GetFromJsonAsync<ReportDetailsDto>($"/api/Reports/details/{reportId}");
    }

    public async Task<PagedResultDto<ReportItemDto>> GetReportItems(Guid reportId, int page)
    {
        return (await _httpClient.GetFromJsonAsync<PagedResultDto<ReportItemDto>>(
            $"/api/Reports/details/{reportId}/items/{page}"))!;
    }

    public async Task<PagedResultDto<ReportItemDto>> GetReportItemsWithDuplicatedVin(Guid reportId, int page)
    {
        return (await _httpClient.GetFromJsonAsync<PagedResultDto<ReportItemDto>>(
            $"/api/Reports/details/{reportId}/items/{page}/duplicatedVins"))!;
    }
    
    public async Task<PagedResultDto<ReportItemDto>> GetReportItemsElectricsForReport(Guid reportId, int page)
    {
        return (await _httpClient.GetFromJsonAsync<PagedResultDto<ReportItemDto>>(
            $"/api/Reports/details/{reportId}/items/{page}/electrics"))!;
    }

    public async Task<HttpResponseMessage> UpdateReportItemStatus(UpdateReportItemStatusDto updateReportItemStatusDto)
    {
        return await _httpClient.PutAsJsonAsync("/api/reports/item/status", updateReportItemStatusDto);
    }

    public async Task<HttpResponseMessage> UploadFile(Guid reportId, string fileName, Stream fileStream)
    {
        var multipartContent = new MultipartFormDataContent()
        {
            { new StreamContent(fileStream), "file", fileName }
        };
        
        return await _httpClient.PutAsync($"/api/Reports/{reportId}", multipartContent);
    }

    public async Task<HttpResponseMessage> DownloadReport(Guid reportId, string format)
    {
        return await _httpClient.GetAsync($"/api/Reports/download/{reportId}/{format}");
    }
}