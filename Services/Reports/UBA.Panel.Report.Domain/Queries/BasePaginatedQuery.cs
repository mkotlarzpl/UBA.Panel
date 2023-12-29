using MediatR;

namespace UBA.Panel.Report.Domain.Queries;

public abstract record BasePaginatedQuery<T>(int? Page) : IRequest<T>;