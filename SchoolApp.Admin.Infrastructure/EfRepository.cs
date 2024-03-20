using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Application.SeedWork;
using Admin.Infrastructure.Data;
using Ardalis.Specification.EntityFrameworkCore;

namespace Admin.Infrastructure;
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(AdminDbContext dbContext) : base(dbContext)
    {
    }
}