namespace SchoolApp.Admin.Infrastructure.Data;

public class AdminDbContext : DbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public AdminDbContext()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public AdminDbContext(DbContextOptions<AdminDbContext> options, IMediator mediator)
        : base(options)
    {
        
        Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

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
    }
}
