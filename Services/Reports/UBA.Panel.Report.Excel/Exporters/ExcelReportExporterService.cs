using ClosedXML.Excel;
using UBA.Panel.Report.Domain.Data.ValueObjects;
using UBA.Panel.Report.Domain.Interfaces;
using UBA.Panel.Report.Excel.Translations;

namespace UBA.Panel.Report.Excel.Exporters;

public class ExcelReportExporterService : IReportExporterService
{
    public async Task Export(IExportStrategy strategy, MemoryStream memoryStream)
    {
        var report = await strategy.GetItemsToExportAsync();

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Report");
        var currentRow = 1;
        
        PrepareHeaders(worksheet, currentRow);
        currentRow++;

        var reportItems = report.ReportItems.ToList();
        Console.WriteLine(reportItems.Count);
        foreach (var reportItem in reportItems)
        {
            AddRow(reportItem, currentRow, worksheet);
            currentRow++;
        }
        workbook.SaveAs(memoryStream);
    }

    private void PrepareHeaders(IXLWorksheet worksheet, int headerRow)
    {
        worksheet.Cell(headerRow, 1).Value = ExcelTranslations.Id_Label;
        worksheet.Cell(headerRow, 2).Value = ExcelTranslations.Name_Label;
        worksheet.Cell(headerRow, 3).Value = ExcelTranslations.RegistrationDate_Label;
        worksheet.Cell(headerRow, 4).Value = ExcelTranslations.Vin_Label;
        worksheet.Cell(headerRow, 5).Value = ExcelTranslations.TBD_Label;
        worksheet.Cell(headerRow, 6).Value = ExcelTranslations.IsElectril_Label;
        worksheet.Cell(headerRow, 7).Value = ExcelTranslations.MappedVehicleClass_Label;
        worksheet.Cell(headerRow, 8).Value = ExcelTranslations.CreatedAt_Label;
        worksheet.Cell(headerRow, 9).Value = ExcelTranslations.Remarks_Label;
        worksheet.Cell(headerRow, 10).Value = ExcelTranslations.Value_Label;
    }

    private void AddRow(ReportItem reportItem, int row, IXLWorksheet worksheet)
    {
        worksheet.Cell(row, 1).Value = reportItem.RowId.ToString();
        worksheet.Cell(row, 2).Value = string.Join(' ', reportItem.FirstName, reportItem.CompanyName);
        worksheet.Cell(row, 3).Value = reportItem.RegistrationDate.ToString("dd-MM-yyyy");
        worksheet.Cell(row, 4).Value = reportItem.Vin;
        worksheet.Cell(row, 5).Value = string.Empty;
        worksheet.Cell(row, 6).Value = reportItem.IsElectric ? ExcelTranslations.True_Value : ExcelTranslations.False_Value;
        worksheet.Cell(row, 7).Value = reportItem.MappedVehicleClass;
        worksheet.Cell(row, 8).Value = reportItem.CreatedAt.ToString("dd-MM-yyyy");
        worksheet.Cell(row, 9).Value = string.Empty;
        worksheet.Cell(row, 10).Value = string.Empty;
    }
}