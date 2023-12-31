using System.Net;
using Microsoft.AspNetCore.Mvc;
using UBA.Panel.Report.Api.Client;
using UBA.Panel.Report.Common.DTOs;

namespace UBA.Panel.Frontend.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportApiClient _reportApiClient;
        
        public ReportsController(IReportApiClient reportApiClient)
        {
            _reportApiClient = reportApiClient;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport(CreateReportDto createReportDto)
        {
            var response = await _reportApiClient.CreateReportAsync(createReportDto);
            
            if (!response.IsSuccessStatusCode)
            {
                return response.StatusCode == HttpStatusCode.BadRequest ? BadRequest(await response.Content.ReadAsStringAsync()) : Problem(await response.Content.ReadAsStringAsync());
            }
            
            return Ok();
        }

        [HttpGet]
        [Route("all/{page}")]
        public async Task<IActionResult> GetReports(int page)
        {
            var result = await _reportApiClient.GetReports(page);
            return Ok(result);
        }

        [HttpGet]
        [Route("details/{reportId}")]
        public async Task<IActionResult> GetReportDetails(Guid reportId)
        {
            var result = await _reportApiClient.GetReportDetails(reportId);

            if (result == null)
            {
                return NotFound();
            }
            
            return Ok(result);
        }

        [HttpGet]
        [Route("details/{reportId}/items/{page}")]
        public async Task<IActionResult> GetReportItems(Guid reportId, int page)
        {
            var result = await _reportApiClient.GetReportItems(reportId, page);
            return Ok(result);
        }
        
        [HttpGet]
        [Route("details/{reportId}/items/{page}/duplicatedVins")]
        public async Task<IActionResult> GetReportItemsWithDuplicatedVinsForReport(Guid reportId, int page)
        {
            var result = await _reportApiClient.GetReportItemsWithDuplicatedVin(reportId, page);
            return Ok(result);
        }
        
        [HttpGet]
        [Route("details/{reportId}/items/{page}/electrics")]
        public async Task<IActionResult> GetReportItemsElectricsForReport(Guid reportId, int page)
        {
            var result = await _reportApiClient.GetReportItemsElectricsForReport(reportId, page);
            
            return Ok(result);
        }
        
        [HttpPut]
        [Route("item/status")]
        public async Task<IActionResult> UpdateReportItemStatus([FromBody] UpdateReportItemStatusDto updateReportItemStatusDto)
        {
            var response = await _reportApiClient.UpdateReportItemStatus(updateReportItemStatusDto);
            
            if (!response.IsSuccessStatusCode)
            {
                return response.StatusCode == HttpStatusCode.BadRequest ? BadRequest(await response.Content.ReadAsStringAsync()) : Problem(await response.Content.ReadAsStringAsync());
            }
            
            return Ok(response);
        }

        [HttpPut]
        [Route("{reportId}")]
        public async Task<IActionResult> AddFileToReport(Guid reportId, IFormFile file)
        {
            await using var fileStream = file.OpenReadStream();
            var response = await _reportApiClient.UploadFile(reportId, file.FileName, fileStream);
            
            if (!response.IsSuccessStatusCode)
            {
                return response.StatusCode == HttpStatusCode.BadRequest ? BadRequest(await response.Content.ReadAsStringAsync()) : Problem(await response.Content.ReadAsStringAsync());
            }
            
            return Ok();
        }

        [HttpGet]
        [Route("download/{reportId}/{fileName}/{format}")]
        public async Task<IActionResult> DownloadReport(string reportId, string fileName, string format)
        {
            var response = await _reportApiClient.DownloadReport(Guid.Parse(reportId), format);

            if (!response.IsSuccessStatusCode)
            {
                return Problem();
            }
            
            return File(await response.Content.ReadAsStreamAsync(), 
                "application/octet-stream", 
                $"{fileName}.{format}");
        }
    }
}
