using Admin.Web.Models;
using SchoolApp.Admin.Web.Services;

namespace Admin.Web.Services;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class FacultyService
{
    private readonly HttpClient _httpClient;
    private readonly string? _serviceEndpoint;


    public FacultyService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _serviceEndpoint = $"{config.GetValue<string>("BackendUrl")}/Faculty";
    }

    public async Task<IEnumerable<Faculty>?> GetAllFacultiesAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ListFacultiesResponse>(_serviceEndpoint);
        return response?.Faculties ?? Enumerable.Empty<Faculty>();
    }

    public async Task<Faculty?> GetFacultyByIdAsync(int facultyId)
    {
        return await _httpClient.GetFromJsonAsync<Faculty>($"{_serviceEndpoint}/{facultyId}");
    }

    public async Task<HttpResponseMessage> AddFacultyAsync(Faculty? faculty)
    {
        return await _httpClient.PostAsJsonAsync(_serviceEndpoint, faculty);
    }

    public async Task<HttpResponseMessage> UpdateFacultyAsync(int facultyId, Faculty? faculty)
    {
        return await _httpClient.PutAsJsonAsync($"{_serviceEndpoint}/{facultyId}", faculty);
    }

    public async Task<HttpResponseMessage> DeleteFacultyAsync(int facultyId)
    {
        return await _httpClient.DeleteAsync($"{_serviceEndpoint}/{facultyId}");
    }
}
