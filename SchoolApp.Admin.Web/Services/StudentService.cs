using SchoolApp.Admin.Web.Services;

namespace Admin.Web.Services;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Admin.Web.Models; // Adjust the namespace to where your models are located
using System.Collections.Generic;

public class StudentService
{
    private readonly HttpClient _httpClient;
    private readonly string? _serviceEndpoint;


    public StudentService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _serviceEndpoint = $"{config.GetValue<string>("BackendUrl")}/Student";
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ListStudentsResponse>(_serviceEndpoint);
        return response?.Students ?? Enumerable.Empty<Student>();
    }

    public async Task<Student?> GetStudentByIdAsync(int studentId)
    {
        return await _httpClient.GetFromJsonAsync<Student>($"{_serviceEndpoint}/{studentId}");
    }

    public async Task<HttpResponseMessage> AddStudentAsync(Student? student)
    {
        return await _httpClient.PostAsJsonAsync(_serviceEndpoint, student);
    }

    public async Task<HttpResponseMessage> UpdateStudentAsync(int studentId, Student? student)
    {
        return await _httpClient.PutAsJsonAsync($"{_serviceEndpoint}/{studentId}", student);
    }

    public async Task<HttpResponseMessage> DeleteStudentAsync(int studentId)
    {
        return await _httpClient.DeleteAsync($"{_serviceEndpoint}/{studentId}");
    }
}
