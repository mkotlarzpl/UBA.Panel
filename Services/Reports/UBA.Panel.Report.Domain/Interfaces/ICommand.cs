using MediatR;

namespace UBA.Panel.Report.Domain.Interfaces;

public interface ICommand<T> : IRequest<T>
{
}