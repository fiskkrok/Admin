using SchoolApp.Admin.Web.Services;

namespace Admin.Web.Services;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Models; // Adjust the namespace to where your models are located
using System.Collections.Generic;

public class CourseAssignmentService(HttpClient httpClient, IConfiguration config)
{
    private readonly string? _serviceEndpoint = $"{config.GetValue<string>("BackendUrl")}/CourseAssignment";


    public async Task<IEnumerable<CourseAssignment>?> GetAllCourseAssignmentsAsync()
    {
        var response = await httpClient.GetFromJsonAsync<ListCourseAssignmentsResponse>(_serviceEndpoint);
        return response?.CourseAssignments ?? Enumerable.Empty<CourseAssignment>();
    }

    public async Task<CourseAssignment?> GetCourseAssignmentByIdAsync(int assignmentId)
    {
        return await httpClient.GetFromJsonAsync<CourseAssignment>($"{_serviceEndpoint}{assignmentId}");
    }

    public async Task<HttpResponseMessage> AddCourseAssignmentAsync(CourseAssignment? courseAssignment)
    {
        return await httpClient.PostAsJsonAsync(_serviceEndpoint, courseAssignment);
    }

    public async Task<HttpResponseMessage> UpdateCourseAssignmentAsync(int assignmentId,
        CourseAssignment? courseAssignment)
    {
        return await httpClient.PutAsJsonAsync($"{_serviceEndpoint}/{assignmentId}", courseAssignment);
    }

    public async Task<HttpResponseMessage> DeleteCourseAssignmentAsync(int assignmentId)
    {
        return await httpClient.DeleteAsync($"{_serviceEndpoint}/{assignmentId}");
    }
}
