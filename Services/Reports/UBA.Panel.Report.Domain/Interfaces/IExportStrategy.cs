namespace UBA.Panel.Report.Domain.Interfaces;

public interface IExportStrategy
{
    Task<Data.AggregateRoots.Report> GetItemsToExportAsync();
}