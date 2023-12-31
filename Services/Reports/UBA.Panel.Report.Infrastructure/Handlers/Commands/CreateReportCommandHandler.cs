using MediatR;
using Microsoft.Extensions.Logging;
using UBA.Panel.Report.Domain.Commands;
using UBA.Panel.Report.Domain.Exceptions;
using UBA.Panel.Report.Domain.Interfaces;

namespace UBA.Panel.Report.Infrastructure.Handlers.Commands;

public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, Guid>
{
    private readonly IReportsRepository _repository;
    private readonly ILogger<CreateReportCommandHandler> _logger;

    public CreateReportCommandHandler(IReportsRepository repository, ILogger<CreateReportCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<Guid> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        var report = new Domain.Data.AggregateRoots.Report(request.Name);
        var existingReport = await _repository.GetReportAsync((r) => r.Name == request.Name);

        if (existingReport != null)
        {
            throw new NotUniqueEntryException($"Report with name: {request.Name} already exists");
        }
        
        var result = await _repository.CreateReportAsync(report);

        return result.Id;
    }
}