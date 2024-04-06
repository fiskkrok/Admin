namespace SchoolApp.Admin.Infrastructure.EntityConfigurations;

public class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> entity)
    {
        entity.HasKey(b => b.CourseId);
        entity.HasIndex(e => e.Id); // Create an index on the Id property
        entity.Property(b => b.Id).ValueGeneratedOnAdd(); // Configure EF to auto-generate Id values
        entity.Property(e => e.CourseId).ValueGeneratedNever();
        // No direct reference to Enrollments or CourseAssignments needed; those are managed via their own configurations
    }
}