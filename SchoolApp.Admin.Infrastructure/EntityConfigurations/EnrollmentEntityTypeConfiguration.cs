
namespace SchoolApp.Admin.Infrastructure.EntityConfigurations;

public class EnrollmentEntityTypeConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> entity)
    {
        entity.HasKey(b => b.Id);

        entity.Property(b => b.Id)
            .ValueGeneratedOnAdd(); // Configure EF to auto-generate Id values
        entity.HasAlternateKey(e => e.EnrollmentId);

        entity.Property(e => e.EnrollmentId).ValueGeneratedNever();

        entity.HasOne(d => d.Course).WithMany(p => p.Enrollments);

        entity.HasOne(d => d.Student).WithMany(p => p.Enrollments);

    }
}