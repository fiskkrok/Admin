

using SchoolApp.Admin.Web.Models;
namespace SchoolApp.Admin.Web.Services;

public class StudentService(HttpClient httpClient)
{
    private const string ServiceEndpoint = "/api/v1/admin/Student";


    public async Task<List<Student>> GetAllStudentsAsync()
    {
        var students = await httpClient.GetFromJsonAsync<List<Student>>(ServiceEndpoint);
        return students ?? [];
    }

    public async Task<Student?> GetStudentByIdAsync(string studentId)
    {
        return await httpClient.GetFromJsonAsync<Student>($"{ServiceEndpoint}/{studentId}");
    }

    public async Task<HttpResponseMessage> AddStudentAsync(Student? student)
    {
        return await httpClient.PostAsJsonAsync(ServiceEndpoint, student);
    }

    public async Task<HttpResponseMessage> UpdateStudentAsync(string studentId, Student? student)
    {
        return await httpClient.PutAsJsonAsync($"{ServiceEndpoint}/{studentId}", student);
    }

    public async Task<HttpResponseMessage> DeleteStudentAsync(string studentId)
    {
        return await httpClient.DeleteAsync($"{ServiceEndpoint}/{studentId}");
    }
}
