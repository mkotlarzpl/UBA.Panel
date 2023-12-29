namespace UBA.Panel.Report.Domain.Interfaces;

public interface IFileProcessorService
{
    Task Process(Guid reportId, string fileName, Stream file);
}