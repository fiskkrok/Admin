

using SchoolApp.Admin.Web.Models;
namespace SchoolApp.Admin.Web.Services;

public class StudentService(HttpClient httpClient)
{
    private const string remoteServiceBaseUrl = "/api/v1/admin/Student";


    public async Task<List<Student>> GetAllStudentsAsync()
    {
        var students = await httpClient.GetFromJsonAsync<List<Student>>(remoteServiceBaseUrl);
        return students ?? [];
    }

    public async Task<Student?> GetStudentByIdAsync(string studentId)
    {
        return await httpClient.GetFromJsonAsync<Student>($"{remoteServiceBaseUrl}/{studentId}");
    }

    public async Task<HttpResponseMessage> AddStudentAsync(Student? student)
    {
        return await httpClient.PostAsJsonAsync(remoteServiceBaseUrl, student);
    }

    public async Task<HttpResponseMessage> UpdateStudentAsync(string studentId, Student? student)
    {
        return await httpClient.PutAsJsonAsync($"{remoteServiceBaseUrl}/{studentId}", student);
    }

    public async Task<HttpResponseMessage> DeleteStudentAsync(string studentId)
    {
        return await httpClient.DeleteAsync($"{remoteServiceBaseUrl}/{studentId}");
    }
}
