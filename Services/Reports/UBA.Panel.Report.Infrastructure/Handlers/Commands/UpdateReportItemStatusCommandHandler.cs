using MediatR;
using Microsoft.Extensions.Logging;
using UBA.Panel.Report.Domain.Commands;
using UBA.Panel.Report.Domain.Interfaces;

namespace UBA.Panel.Report.Infrastructure.Handlers.Commands;

public class UpdateReportItemStatusCommandHandler : IRequestHandler<UpdateReportItemStatusCommand>
{
    private readonly IReportsRepository _repository;

    public UpdateReportItemStatusCommandHandler(IReportsRepository repository)
    {
        _repository = repository;
    }
    
    public async Task Handle(UpdateReportItemStatusCommand request, CancellationToken cancellationToken)
    {
        await _repository.UpdateReportItemStatus(request.ReportItemId, request.Status);
    }
}