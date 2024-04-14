namespace SchoolApp.Admin.Web.Services;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
// Adjust the namespace to where your models are located
using System.Collections.Generic;
using SchoolApp.Admin.Web.Models;

public class CourseService(HttpClient httpClient)
{
    private const string? ServiceEndpoint = "/api/v1/admin//Course";


    public async Task<List<Course>?> GetAllCoursesAsync()
    {

        var courses = await httpClient.GetFromJsonAsync<List<Course>>(ServiceEndpoint);
        return courses ?? [];
    }

    public async Task<Course?> GetCourseByIdAsync(int courseId)
    {
        return await httpClient.GetFromJsonAsync<Course>($"{ServiceEndpoint}{courseId}");
    }

    public async Task<HttpResponseMessage> AddCourseAsync(Course? course)
    {
        return await httpClient.PostAsJsonAsync(ServiceEndpoint, course);
    }

    public async Task<HttpResponseMessage> UpdateCourseAsync(int courseId, Course? course)
    {
        return await httpClient.PutAsJsonAsync($"{ServiceEndpoint}/{courseId}", course);
    }

    public async Task<HttpResponseMessage> DeleteCourseAsync(int courseId)
    {
        return await httpClient.DeleteAsync($"{ServiceEndpoint}/{courseId}");
    }
}