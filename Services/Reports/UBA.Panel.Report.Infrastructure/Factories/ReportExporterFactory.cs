using UBA.Panel.Report.Domain.Interfaces;
using UBA.Panel.Report.Excel.Exporters;
using UBA.Panel.Report.Infrastructure.Interfaces;

namespace UBA.Panel.Report.Infrastructure.Factories;

public class ReportExporterFactory : IReportExporterFactory
{
    public IReportExporterService Create(string format)
    {
        if (format.ToLower() == "xlsx")
        {
            return new ExcelReportExporterService();
        }
        
        throw new NotImplementedException();
    }
}