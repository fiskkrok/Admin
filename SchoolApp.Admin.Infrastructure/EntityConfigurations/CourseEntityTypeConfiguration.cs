
namespace SchoolApp.Admin.Infrastructure.EntityConfigurations;

public class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> entity)
    {
        entity.HasKey(b => b.Id);

        entity.Property(b => b.Id)
            .ValueGeneratedOnAdd(); // Configure EF to auto-generate Id values
        entity.HasAlternateKey(e => e.CourseId);
        entity.Property(e => e.CourseId).ValueGeneratedNever();
    }
}