namespace SchoolApp.Admin.Web.Services;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Models; // Ensure this matches the namespace of your models
using System.Collections.Generic;
using SchoolApp.Admin.Web.Models;

public class EnrollmentService(HttpClient httpClient)
{
    private const string remoteServiceBaseUrl = "/api/v1/admin/Enrollment";


    public async Task<List<Enrollment>?> GetAllEnrollmentsAsync()
    {
        var enrollments = await httpClient.GetFromJsonAsync<List<Enrollment>>(remoteServiceBaseUrl);
        return enrollments ?? [];
    }

    public async Task<Enrollment?> GetEnrollmentByIdAsync(string enrollmentId)
    {
        return await httpClient.GetFromJsonAsync<Enrollment>($"{remoteServiceBaseUrl}/{enrollmentId}");
    }

    public async Task<HttpResponseMessage> AddEnrollmentAsync(Enrollment? enrollment)
    {
        return await httpClient.PostAsJsonAsync(remoteServiceBaseUrl, enrollment);
    }

    public async Task<HttpResponseMessage> UpdateEnrollmentAsync(string enrollmentId, Enrollment? enrollment)
    {
        return await httpClient.PutAsJsonAsync($"{remoteServiceBaseUrl}/{enrollmentId}", enrollment);
    }

    public async Task<HttpResponseMessage> DeleteEnrollmentAsync(string? enrollmentId)
    {
        return await httpClient.DeleteAsync($"{remoteServiceBaseUrl}/{enrollmentId}");
    }
}

