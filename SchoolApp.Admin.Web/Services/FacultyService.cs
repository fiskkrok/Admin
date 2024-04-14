namespace SchoolApp.Admin.Web.Services;

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using Models;

public class FacultyService(HttpClient httpClient)
{
    private const string ServiceEndpoint = "/api/v1/admin/Faculty";


    public async Task<List<Faculty>?> GetAllFacultiesAsync()
    {
        var faculties = await httpClient.GetFromJsonAsync<List<Faculty>>(ServiceEndpoint);
        return faculties ?? [];
    }

    public async Task<Faculty?> GetFacultyByIdAsync(string facultyId)
    {
        return await httpClient.GetFromJsonAsync<Faculty>($"{ServiceEndpoint}/{facultyId}");
    }

    public async Task<HttpResponseMessage> AddFacultyAsync(Faculty? faculty)
    {
        return await httpClient.PostAsJsonAsync(ServiceEndpoint, faculty);
    }

    public async Task<HttpResponseMessage> UpdateFacultyAsync(string facultyId, Faculty? faculty)
    {
        return await httpClient.PutAsJsonAsync($"{ServiceEndpoint}/{facultyId}", faculty);
    }

    public async Task<HttpResponseMessage> DeleteFacultyAsync(string facultyId)
    {
        return await httpClient.DeleteAsync($"{ServiceEndpoint}/{facultyId}");
    }
}
