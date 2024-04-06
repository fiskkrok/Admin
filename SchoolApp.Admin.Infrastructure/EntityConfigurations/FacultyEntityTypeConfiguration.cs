namespace SchoolApp.Admin.Infrastructure.EntityConfigurations;

public class FacultyEntityTypeConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> entity)
    {
        entity.HasKey(b => b.FacultyId);
        entity.HasIndex(e => e.Id); // Create an index on the Id property
        entity.Property(b => b.Id).ValueGeneratedOnAdd(); // Configure EF to auto-generate Id values
        entity.Property(e => e.FacultyId).ValueGeneratedNever();
        // No change needed here; Faculty relationships are managed in CourseAssignment
    }
}