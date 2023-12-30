using MediatR;
using Microsoft.AspNetCore.Mvc;
using UBA.Panel.Report.Common.DTOs;
using UBA.Panel.Report.Domain.Commands;
using UBA.Panel.Report.Domain.Queries;

namespace UBA.Panel.Report.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateReport(CreateReportDto createReportDto)
        {
            try
            {
                var command = new CreateReportCommand(createReportDto.Name);
                var reportId = await _mediator.Send(command);

                return Created($"/api/reports/{reportId}", null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{reportName}")]
        public async Task<IActionResult> GetReportAsync(string reportName)
        {
            try
            {
                var query = new GetReportQuery(reportName);
                var report = await _mediator.Send(query);

                if (report == null)
                {
                    return NotFound($"Report with Name: {reportName} not found");
                }

                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("all/{page}")]
        public async Task<IActionResult> GetReports(int? page)
        {
            try
            {
                var query = new GetReportsQuery(page);
                var reports = await _mediator.Send(query);

                return Ok(reports);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        [Route("details/{reportId}")]
        public async Task<IActionResult> GetReportDetails(Guid reportId)
        {
            try
            {
                var query = new GetReportDetailsQuery(reportId);
                var report = await _mediator.Send(query);

                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        [Route("details/{reportId}/items/{page}")]
        public async Task<IActionResult> GetReportItemsForReport(Guid reportId, int page) 
        {
            try
            {
                var query = new GetReportItemsForReportQuery(reportId, page);
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        [Route("details/{reportId}/items/{page}/duplicatedVins")]
        public async Task<IActionResult> GetReportItemsWithDuplicatedVinsForReport(Guid reportId, int page) 
        {
            try
            {
                var query = new GetReportItemsWithDuplicatedVinsForReportQuery(reportId, page);
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        [Route("details/{reportId}/items/{page}/electrics")]
        public async Task<IActionResult> GetReportItemsElectricsForReport(Guid reportId, int page) 
        {
            try
            {
                var query = new GetReportItemsElectricsForReportQuery(reportId, page);
                var result = await _mediator.Send(query);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{reportId}")]
        public async Task<IActionResult> AddFileToReport(string reportId, IFormFile file)
        {
            try
            {
                var fileStream = file.OpenReadStream();
                var command = new AddFileToReportCommand(Guid.Parse(reportId), file.FileName, fileStream);
                await _mediator.Send(command);
                fileStream.Close();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("item/status")]
        public async Task<IActionResult> UpdateReportItemStatus([FromBody] UpdateReportItemStatusDto updateReportItemStatusDto)
        {
            try
            {
                var command = new UpdateReportItemStatusCommand(updateReportItemStatusDto.ReportItemId,
                    updateReportItemStatusDto.Status);
                await _mediator.Send(command);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
