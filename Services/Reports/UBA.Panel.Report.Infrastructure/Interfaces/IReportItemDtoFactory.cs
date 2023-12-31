using UBA.Panel.Report.Common.DTOs;
using UBA.Panel.Report.Domain.Data.ValueObjects;

namespace UBA.Panel.Report.Infrastructure.Interfaces;

public interface IReportItemDtoFactory
{
    ReportItemDto Create(ReportItem reportItem);
}