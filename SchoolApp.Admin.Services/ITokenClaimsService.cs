using System.Threading.Tasks;

namespace SchoolApp.Admin.Services;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string userName);
}
