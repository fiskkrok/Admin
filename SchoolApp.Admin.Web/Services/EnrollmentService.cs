using SchoolApp.Admin.Web.Services;

namespace Admin.Web.Services;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Models; // Ensure this matches the namespace of your models
using System.Collections.Generic;

public class EnrollmentService
{
    private readonly HttpClient _httpClient;
    private readonly string? _serviceEndpoint;


    public EnrollmentService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _serviceEndpoint = $"{config.GetValue<string>("BackendUrl")}/Enrollment";
    }

    public async Task<IEnumerable<Enrollment>?> GetAllEnrollmentsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ListEnrollmentsResponse>(_serviceEndpoint);
        return response?.Enrollments ?? Enumerable.Empty<Enrollment>();
    }

    public async Task<Enrollment?> GetEnrollmentByIdAsync(int enrollmentId)
    {
        return await _httpClient.GetFromJsonAsync<Enrollment>($"{_serviceEndpoint}/{enrollmentId}");
    }

    public async Task<HttpResponseMessage> AddEnrollmentAsync(Enrollment? enrollment)
    {
        return await _httpClient.PostAsJsonAsync(_serviceEndpoint, enrollment);
    }

    public async Task<HttpResponseMessage> UpdateEnrollmentAsync(int enrollmentId, Enrollment? enrollment)
    {
        return await _httpClient.PutAsJsonAsync($"{_serviceEndpoint}/{enrollmentId}", enrollment);
    }

    public async Task<HttpResponseMessage> DeleteEnrollmentAsync(int enrollmentId)
    {
        return await _httpClient.DeleteAsync($"{_serviceEndpoint}/{enrollmentId}");
    }
}

