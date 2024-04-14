using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;

using SchoolApp.IntegrationEventLogEF;
using IsolationLevel = System.Data.IsolationLevel;

namespace SchoolApp.Admin.Infrastructure.Data;

public class AdminDbContext : DbContext, IUnitOfWork
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public AdminDbContext()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
    private readonly IMediator _mediator;
    public AdminDbContext(DbContextOptions<AdminDbContext> options, IMediator mediator)
        : base(options)
    {

        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


        System.Diagnostics.Debug.WriteLine("AdminDbContext::ctor ->" + this.GetHashCode());
    }
    private IDbContextTransaction _currentTransaction;
    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;
    public IMediator Mediator { get; }
    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseAssignment> CourseAssignments { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;
        SqlConnectionStringBuilder builder = new()
        {
            DataSource = "FISKKROK\\SQLEXPRESS",
            InitialCatalog = "AdminSystem",
            TrustServerCertificate = true,
            MultipleActiveResultSets = true,
            // Because we want to fail faster. Default is 15 seconds.
            ConnectTimeout = 15,
            IntegratedSecurity = true
        };
        optionsBuilder.UseSqlServer(builder.ConnectionString)
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfiguration(new CourseAssignmentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CourseEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EnrollmentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new FacultyEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new StudentEntityTypeConfiguration());
        modelBuilder.UseIntegrationEventLogs();
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _mediator.DispatchDomainEventsAsync(this);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        _ = await base.SaveChangesAsync(cancellationToken);

        return true;
    }
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}

