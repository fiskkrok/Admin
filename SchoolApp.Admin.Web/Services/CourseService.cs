using SchoolApp.Admin.Web.Services;

namespace Admin.Web.Services;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
// Adjust the namespace to where your models are located
using System.Collections.Generic;
using Admin.Web.Models;

public class CourseService
{
    private readonly HttpClient _httpClient;
    private readonly string? _serviceEndpoint;


    public CourseService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _serviceEndpoint = $"{config.GetValue<string>("BackendUrl")}/Course";
    }
    public async Task<IEnumerable<Course>?> GetAllCoursesAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ListCoursesResponse>(_serviceEndpoint);
        return response?.Courses ?? Enumerable.Empty<Course>();
    }

    public async Task<Course?> GetCourseByIdAsync(int courseId)
    {
        return await _httpClient.GetFromJsonAsync<Course>($"{_serviceEndpoint}{courseId}");
    }

    public async Task<HttpResponseMessage> AddCourseAsync(Course? course)
    {
        return await _httpClient.PostAsJsonAsync(_serviceEndpoint, course);
    }

    public async Task<HttpResponseMessage> UpdateCourseAsync(int courseId, Course? course)
    {
        return await _httpClient.PutAsJsonAsync($"{_serviceEndpoint}/{courseId}", course);
    }

    public async Task<HttpResponseMessage> DeleteCourseAsync(int courseId)
    {
        return await _httpClient.DeleteAsync($"{_serviceEndpoint}/{courseId}");
    }
}