

namespace SchoolApp.Admin.Domain.SeedWork;

public interface ITokenClaimsService
{
    Task<string> GetTokenAsync(string userName);
}
