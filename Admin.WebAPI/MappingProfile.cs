using Admin.Application.AggregateModels.CourseAggregate;
using Admin.Application.AggregateModels.CourseAssignmentAggregate;
using Admin.Application.AggregateModels.EnrollmentAggregate;
using Admin.Application.AggregateModels.FacultyAggregate;
using Admin.Application.AggregateModels.StudentAggregate;
using Admin.WebAPI.Endpoints.Course;
using Admin.WebAPI.Endpoints.CourseAssignment;
using Admin.WebAPI.Endpoints.Enrollment;
using Admin.WebAPI.Endpoints.Faculty;
using Admin.WebAPI.Endpoints.Student;
using AutoMapper;

namespace Admin.WebAPI;

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