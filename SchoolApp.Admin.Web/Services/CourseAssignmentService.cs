namespace SchoolApp.Admin.Web.Services;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Models; // Adjust the namespace to where your models are located
using System.Collections.Generic;

public class CourseAssignmentService(HttpClient httpClient)
{
    private const string remoteServiceBaseUrl = "/api/v1/admin/CourseAssignment";

    public async Task<List<CourseAssignment>> GetAllCourseAssignmentsAsync()
    {
        var courseAssignments = await httpClient.GetFromJsonAsync<List<CourseAssignment>>(remoteServiceBaseUrl);
        return courseAssignments ?? [];
    }

    public async Task<CourseAssignment> GetCourseAssignmentByIdAsync(string assignmentId)
    {
        return await httpClient.GetFromJsonAsync<CourseAssignment>($"{remoteServiceBaseUrl}/{assignmentId}");
    }

    public async Task<HttpResponseMessage> AddCourseAssignmentAsync(CourseAssignment courseAssignment)
    {
        return await httpClient.PostAsJsonAsync(remoteServiceBaseUrl, courseAssignment);
    }

    public async Task<HttpResponseMessage> UpdateCourseAssignmentAsync(string assignmentId, CourseAssignment courseAssignment)
    {
        return await httpClient.PutAsJsonAsync($"{remoteServiceBaseUrl}/{assignmentId}", courseAssignment);
    }

    public async Task<HttpResponseMessage> DeleteCourseAssignmentAsync(string assignmentId)
    {
        return await httpClient.DeleteAsync($"{remoteServiceBaseUrl}/{assignmentId}");
    }
}
