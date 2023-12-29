using UBA.Panel.Report.Domain.Interfaces;

namespace UBA.Panel.Report.Domain.Commands;

public record CreateReportCommand(string Name) : ICommand<Guid>;