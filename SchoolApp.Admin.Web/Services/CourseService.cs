namespace SchoolApp.Admin.Web.Services;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
// Adjust the namespace to where your models are located
using System.Collections.Generic;
using SchoolApp.Admin.Web.Models;

public class CourseService(HttpClient httpClient)
{
    private readonly string remoteServiceBaseUrl = "/api/v1/admin/Course";


    public async Task<List<Course>?> GetAllCoursesAsync()
    {

        var courses = await httpClient.GetFromJsonAsync<List<Course>>(remoteServiceBaseUrl);
        return courses ?? [];
    }

    public async Task<Course?> GetCourseByIdAsync(int courseId)
    {
        return await httpClient.GetFromJsonAsync<Course>($"{remoteServiceBaseUrl}{courseId}");
    }

    public async Task<HttpResponseMessage> AddCourseAsync(Course? course)
    {
        return await httpClient.PostAsJsonAsync(remoteServiceBaseUrl, course);
    }

    public async Task<HttpResponseMessage> UpdateCourseAsync(int courseId, Course? course)
    {
        return await httpClient.PutAsJsonAsync($"{remoteServiceBaseUrl}/{courseId}", course);
    }

    public async Task<HttpResponseMessage> DeleteCourseAsync(int courseId)
    {
        return await httpClient.DeleteAsync($"{remoteServiceBaseUrl}/{courseId}");
    }
}