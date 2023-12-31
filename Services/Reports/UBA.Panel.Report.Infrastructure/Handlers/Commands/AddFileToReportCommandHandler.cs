using MediatR;
using Microsoft.Extensions.Logging;
using UBA.Panel.Report.Domain.Commands;
using UBA.Panel.Report.Domain.Interfaces;

namespace UBA.Panel.Report.Infrastructure.Handlers.Commands;

public class AddFileToReportCommandHandler : IRequestHandler<AddFileToReportCommand>
{
    private readonly IFileUploaderService _fileUploader;

    public AddFileToReportCommandHandler(IFileUploaderService fileUploader)
    {
        _fileUploader = fileUploader;
    }
    
    public async Task Handle(AddFileToReportCommand request, CancellationToken cancellationToken)
    {
        await _fileUploader.UploadFileToProcess(request.ReportId, request.FileName, request.File);
    }
}