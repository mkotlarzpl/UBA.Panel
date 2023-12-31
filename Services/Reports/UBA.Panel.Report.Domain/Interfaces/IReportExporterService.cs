namespace UBA.Panel.Report.Domain.Interfaces;

public interface IReportExporterService
{
    Task Export(IExportStrategy strategy, MemoryStream memoryStream);
}