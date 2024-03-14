using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Application.AggregateModels.StudentAggregate;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Admin.Infrastructure.EntityConfigurations;

public class StudentEntityTypeConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> entity)
    {
        entity.HasKey(e => e.StudentId);

        entity.Property(e => e.StudentId).ValueGeneratedNever();

        entity.OwnsOne(o => o.Address);

    }
}
