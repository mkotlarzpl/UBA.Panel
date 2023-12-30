using MediatR;
using Microsoft.Extensions.Logging;
using UBA.Panel.Report.Domain.Commands;
using UBA.Panel.Report.Domain.Interfaces;

namespace UBA.Panel.Report.Infrastructure.Handlers.Commands;

public class UpdateReportItemStatusCommandHandler : IRequestHandler<UpdateReportItemStatusCommand>
{
    private readonly IReportsRepository _repository;
    private readonly ILogger<UpdateReportItemStatusCommandHandler> _logger;

    public UpdateReportItemStatusCommandHandler(IReportsRepository repository, ILogger<UpdateReportItemStatusCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task Handle(UpdateReportItemStatusCommand request, CancellationToken cancellationToken)
    {
        await _repository.UpdateReportItemStatus(request.ReportItemId, request.Status);
    }
}