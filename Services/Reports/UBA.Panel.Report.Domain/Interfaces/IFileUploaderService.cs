namespace UBA.Panel.Report.Domain.Interfaces;

public interface IFileUploaderService
{
    Task UploadFileToProcess(Guid reportId, string fileName, Stream file, Dictionary<string, string>? metadata = null);
}