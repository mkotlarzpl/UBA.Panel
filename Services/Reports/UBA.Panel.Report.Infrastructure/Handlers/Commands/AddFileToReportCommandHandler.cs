using MediatR;
using Microsoft.Extensions.Logging;
using UBA.Panel.Report.Domain.Commands;
using UBA.Panel.Report.Domain.Interfaces;

namespace UBA.Panel.Report.Infrastructure.Handlers.Commands;

public class AddFileToReportCommandHandler : IRequestHandler<AddFileToReportCommand>
{
    private readonly IFileUploaderService _fileUploader;
    private readonly ILogger<AddFileToReportCommandHandler> _logger;

    public AddFileToReportCommandHandler(IFileUploaderService fileUploader,
        ILogger<AddFileToReportCommandHandler> logger)
    {
        _fileUploader = fileUploader;
        _logger = logger;
    }
    
    public async Task Handle(AddFileToReportCommand request, CancellationToken cancellationToken)
    {
        await _fileUploader.UploadFileToProcess(request.ReportId, request.FileName, request.File);
    }
}