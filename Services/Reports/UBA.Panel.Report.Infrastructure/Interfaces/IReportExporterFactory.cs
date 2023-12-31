using UBA.Panel.Report.Domain.Interfaces;

namespace UBA.Panel.Report.Infrastructure.Interfaces;

public interface IReportExporterFactory
{
    IReportExporterService Create(string format);
}