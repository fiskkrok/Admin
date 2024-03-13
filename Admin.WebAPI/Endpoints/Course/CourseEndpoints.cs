using Admin.Application.Exceptions;
using Admin.Application.SeedWork;
using Admin.Application.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Admin.WebAPI.Endpoints.Course;

public static class CourseEndpoints
{
    public static IEndpointRouteBuilder MapCourseEndpoints(this IEndpointRouteBuilder app)
    {
        // Routes for querying courses
        app.MapGet("/api/Course", GetAllCourses);
        app.MapGet("/api/Course/{id}", GetCourseById);

        // Routes for modifying courses
        app.MapPut("/api/Course/{id}", UpdateCourse);
        app.MapPost("/api/Course", CreateCourse);
        app.MapDelete("/api/Course/{id}", DeleteCourse);

        return app;
    }

    public static async Task<Ok<ListCoursesResponse>> GetAllCourses(IRepository<Application.AggregateModels.CourseAggregate.Course> courseRepository, IMapper mapper)
    {
        var response = new ListCoursesResponse();
        var items = await courseRepository.ListAsync();
        response.Courses.AddRange(items.Select(mapper.Map<CourseRecord>));
        return TypedResults.Ok(response);
    }

    public static async Task<Results<Ok<Application.AggregateModels.CourseAggregate.Course>, NotFound>> GetCourseById(int courseid, IRepository<Application.AggregateModels.CourseAggregate.Course> courseRepository)
    {
        var course = await courseRepository.GetByIdAsync(courseid);
        return course != null ? TypedResults.Ok(course) : TypedResults.NotFound();
    }

    public static async Task<Results<Ok, NotFound>> UpdateCourse(int courseid, Application.AggregateModels.CourseAggregate.Course course, IRepository<Application.AggregateModels.CourseAggregate.Course> courseRepository)
    {
        var existingCourse = await courseRepository.GetByIdAsync(courseid);
        if (existingCourse == null)
        {
            return TypedResults.NotFound();
        }

        existingCourse.CourseCode = course.CourseCode;
        existingCourse.CourseName = course.CourseName;
        existingCourse.Credits = course.Credits;

        await courseRepository.UpdateAsync(existingCourse);
        return TypedResults.Ok();
    }

    public static async Task<IResult> CreateCourse(CreateCourseRequest request, IRepository<Application.AggregateModels.CourseAggregate.Course> courseRepository)
    {
        
        var response = new CreateCourseResponse(request.CorrelationId());

        var courseNameSpecification = new CourseNameSpecification(request.CourseName);
        var existingCourses = await courseRepository.CountAsync(courseNameSpecification);
        if (existingCourses > 0)
        {
            throw new DuplicateException($"A catalogItem with name {request.CourseName} already exists");
        }
        var newCourse = new Application.AggregateModels.CourseAggregate.Course(
            request.CourseId,
            request.Description,
            request.CourseName,
            request.CourseCode,
            request.Credits);
        newCourse = await courseRepository.AddAsync(newCourse);
        if (newCourse.CourseId != 0)
        {
            //We disabled the upload functionality and added a default/placeholder image to this sample due to a potential security risk 
            //  pointed out by the community. More info in this issue: https://github.com/dotnet-architecture/eShopOnWeb/issues/537 
            //  In production, we recommend uploading to a blob storage and deliver the image via CDN after a verification process.

            await courseRepository.UpdateAsync(newCourse);
        }

        var dto = new CourseRecord
        (
            CourseId: newCourse.Id,
            Description: newCourse.Description,
            CourseName: newCourse.CourseName,
            CourseCode: newCourse.CourseCode,
            Credits: newCourse.Credits
        );
        response.Course = dto;
        return Results.Created($"api/course/{dto.CourseId}", response);
    }

    public static async Task<Results<Ok, NotFound>> DeleteCourse(int courseid, IRepository<Application.AggregateModels.CourseAggregate.Course> courseRepository)
    {
        var course = await courseRepository.GetByIdAsync(courseid);
        if (course == null)
        {
            return TypedResults.NotFound();
        }

        await courseRepository.DeleteAsync(course);
        return TypedResults.Ok();
    }
}