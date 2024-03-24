using System.Threading.Tasks;

namespace SchoolApp.Admin.Application.SeedWork;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string userName);
}
