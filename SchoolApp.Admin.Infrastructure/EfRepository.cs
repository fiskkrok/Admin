
using Admin.Infrastructure.Data;
using Ardalis.Specification.EntityFrameworkCore;
using SchoolApp.Admin.Application.SeedWork;

namespace Admin.Infrastructure;
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(AdminDbContext dbContext) : base(dbContext)
    {
    }
}