namespace SchoolApp.Admin.Infrastructure.EntityConfigurations;

public class EnrollmentEntityTypeConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> entity)
    {
        entity.HasKey(b => b.EnrollmentId);
        entity.HasIndex(e => e.Id); // Create an index on the Id property
        entity.Property(b => b.Id).ValueGeneratedOnAdd(); // Configure EF to auto-generate Id values
        entity.Property(e => e.EnrollmentId).ValueGeneratedNever();

        // Correctly establishing relations to Course and Student
        entity.HasOne(d => d.Course).WithMany(p => p.Enrollments).HasForeignKey(d => d.CourseId);
        entity.HasOne(d => d.Student).WithMany(p => p.Enrollments).HasForeignKey(d => d.StudentId);
    }
}