using System;
using System.Collections.Generic;
using Admin.Domain;
using SchoolApp.Admin.Infrastructure.EntityConfigurations;
using MediatR;
using Microsoft.Data.SqlClient;
using SchoolApp.Admin.Application.AggregateModels.CourseAggregate;
using SchoolApp.Admin.Application.AggregateModels.CourseAssignmentAggregate;
using SchoolApp.Admin.Application.AggregateModels.EnrollmentAggregate;
using SchoolApp.Admin.Application.AggregateModels.FacultyAggregate;
using SchoolApp.Admin.Application.AggregateModels.StudentAggregate;

namespace Admin.Infrastructure.Data;

public class AdminDbContext : DbContext
{
    public AdminDbContext()
    {
    }

    public AdminDbContext(DbContextOptions<AdminDbContext> options, IMediator mediator)
        : base(options)
    {
        
        Mediator1 = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public IMediator Mediator1 { get; }
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
