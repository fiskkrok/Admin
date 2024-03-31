
using Ardalis.Specification;

namespace SchoolApp.Admin.Domain.SeedWork;
public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
