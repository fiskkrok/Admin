﻿// <auto-generated />


#nullable disable

namespace Admin.Infrastructure.Migrations
{
    [DbContext(typeof(AdminDbContext))]
    [Migration("20240316195745_First")]
    partial class First
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.2.24128.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Admin.Application.AggregateModels.CourseAggregate.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseCode")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<int>("CourseId")
                        .HasColumnType("int")
                        .HasColumnName("CourseID");

                    b.Property<string>("CourseName")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Credits")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasAlternateKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Admin.Application.AggregateModels.CourseAssignmentAggregate.CourseAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AssignmentId")
                        .HasColumnType("int")
                        .HasColumnName("AssignmentID");

                    b.Property<string>("AssignmentType")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int?>("CourseId")
                        .HasColumnType("int")
                        .HasColumnName("CourseID");

                    b.Property<int?>("FacultyId")
                        .HasColumnType("int")
                        .HasColumnName("FacultyID");

                    b.HasKey("Id");

                    b.HasAlternateKey("AssignmentId");

                    b.HasIndex("CourseId");

                    b.HasIndex("FacultyId");

                    b.ToTable("CourseAssignments");
                });

            modelBuilder.Entity("Admin.Application.AggregateModels.EnrollmentAggregate.Enrollment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CourseId")
                        .HasColumnType("int")
                        .HasColumnName("CourseID");

                    b.Property<DateOnly?>("EnrollmentDate")
                        .HasColumnType("date");

                    b.Property<int>("EnrollmentId")
                        .HasColumnType("int")
                        .HasColumnName("EnrollmentID");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int")
                        .HasColumnName("StudentID");

                    b.HasKey("Id");

                    b.HasAlternateKey("EnrollmentId");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("Admin.Application.AggregateModels.FacultyAggregate.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Department")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("FacultyId")
                        .HasColumnType("int")
                        .HasColumnName("FacultyID");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasAlternateKey("FacultyId");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("Admin.Application.AggregateModels.StudentAggregate.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<DateOnly?>("EnrollmentDate")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("StudentStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Admin.Application.AggregateModels.CourseAssignmentAggregate.CourseAssignment", b =>
                {
                    b.HasOne("Admin.Application.AggregateModels.CourseAggregate.Course", "Course")
                        .WithMany("CourseAssignments")
                        .HasForeignKey("CourseId");

                    b.HasOne("Admin.Application.AggregateModels.FacultyAggregate.Faculty", "Faculty")
                        .WithMany("CourseAssignments")
                        .HasForeignKey("FacultyId");

                    b.Navigation("Course");

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("Admin.Application.AggregateModels.EnrollmentAggregate.Enrollment", b =>
                {
                    b.HasOne("Admin.Application.AggregateModels.CourseAggregate.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseId");

                    b.HasOne("Admin.Application.AggregateModels.StudentAggregate.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId");

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Admin.Application.AggregateModels.StudentAggregate.Student", b =>
                {
                    b.OwnsOne("Admin.Application.AggregateModels.StudentAggregate.Address", "Address", b1 =>
                        {
                            b1.Property<int>("StudentId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(90)
                                .HasColumnType("nvarchar(90)");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasMaxLength(60)
                                .HasColumnType("nvarchar(60)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasMaxLength(180)
                                .HasColumnType("nvarchar(180)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasMaxLength(18)
                                .HasColumnType("nvarchar(18)");

                            b1.HasKey("StudentId");

                            b1.ToTable("Students");

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("Admin.Application.AggregateModels.CourseAggregate.Course", b =>
                {
                    b.Navigation("CourseAssignments");

                    b.Navigation("Enrollments");
                });

            modelBuilder.Entity("Admin.Application.AggregateModels.FacultyAggregate.Faculty", b =>
                {
                    b.Navigation("CourseAssignments");
                });

            modelBuilder.Entity("Admin.Application.AggregateModels.StudentAggregate.Student", b =>
                {
                    b.Navigation("Enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}
