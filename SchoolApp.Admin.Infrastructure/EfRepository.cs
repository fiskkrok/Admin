namespace SchoolApp.Admin.Infrastructure;
public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(AdminDbContext dbContext) : base(dbContext)
    {
    }
}