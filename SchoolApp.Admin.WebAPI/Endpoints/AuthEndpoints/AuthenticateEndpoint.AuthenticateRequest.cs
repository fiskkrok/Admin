using SchoolApp.Admin.WebAPI;

namespace SchoolApp.Admin.WebAPI.Endpoints.AuthEndpoints;

public class AuthenticateRequest : BaseRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
