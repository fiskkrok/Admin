namespace SchoolApp.Admin.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Course, CourseRecord>();
        CreateMap<CourseAssignment, CourseAssignmentRecord>();
        CreateMap<Faculty, FacultyRecord>();
        CreateMap<Student, StudentRecord>();
        CreateMap<Enrollment, EnrollmentRecord>(); 
      
    }
}