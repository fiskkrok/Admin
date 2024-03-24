﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApp.Admin.Application.AggregateModels.CourseAssignmentAggregate;


namespace SchoolApp.Admin.Infrastructure.EntityConfigurations;

public class CourseAssignmentEntityTypeConfiguration : IEntityTypeConfiguration<CourseAssignment>
{
    public void Configure(EntityTypeBuilder<CourseAssignment> entity)
    {

        entity.HasKey(b => b.Id.GetHashCode());

        entity.Property(b => b.Id)
            .ValueGeneratedOnAdd(); // Configure EF to auto-generate Id values
        entity.HasAlternateKey(e => e.AssignmentId);

        entity.Property(e => e.AssignmentId).ValueGeneratedNever();

        entity.HasOne(d => d.Course).WithMany(p => p.CourseAssignments);

        entity.HasOne(d => d.Faculty).WithMany(p => p.CourseAssignments);

    }
}