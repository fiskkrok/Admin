using System;
using System.Collections.Generic;
using Admin.Application.AggregateModels.CourseAggregate;
using Admin.Application.AggregateModels.CourseAssignmentAggregate;
using Admin.Application.AggregateModels.EnrollmentAggregate;
using Admin.Application.AggregateModels.FacultyAggregate;
using Admin.Application.AggregateModels.StudentAggregate;
using Admin.Domain;
using Admin.Infrastructure.EntityConfigurations;

using MediatR;

using Microsoft.Data.SqlClient;

namespace Admin.Infrastructure.Data;

public class AdminDbContext : DbContext
{
    public AdminDbContext()
    {
    }

    public AdminDbContext(DbContextOptions<AdminDbContext> options, IMediator mediator)
        : base(options)
    {
        
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    private readonly IMediator _mediator;
    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseAssignment> CourseAssignments { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            SqlConnectionStringBuilder builder = new();
            builder.DataSource = "FISKKROK\\SQLEXPRESS"; // "ServerName\InstanceName" e.g. @".\sqlexpress"
            builder.InitialCatalog = "AdminSystem";
            builder.TrustServerCertificate = true;
            builder.MultipleActiveResultSets = true;
            // Because we want to fail faster. Default is 15 seconds.
            builder.ConnectTimeout = 3;
            // If using Windows Integrated authentication.
            builder.IntegratedSecurity = true;
            // If using SQL Server authentication.
            // builder.UserID = Environment.GetEnvironmentVariable("MY_SQL_USR");
            // builder.Password = Environment.GetEnvironmentVariable("MY_SQL_PWD");
            optionsBuilder.UseSqlServer(builder.ConnectionString)
                .EnableSensitiveDataLogging();
        }
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
