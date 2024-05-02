namespace SchoolApp.Identity.API;

public class UsersSeed(ILogger<UsersSeed> logger, UserManager<ApplicationUser> userManager)
    : IDbSeeder<ApplicationDbContext>
{
    public async Task SeedAsync(ApplicationDbContext context)
    {
        await EnsureUser("alice", "AliceSmith@email.com", "Pass123$", "Admin");
        await EnsureUser("bob", "BobSmith@email.com", "Pass123$", "TeacherTwo");
    }

    private async Task EnsureUser(string userName, string email, string password, string role)
    {
        var user = await userManager.FindByNameAsync(userName);

        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = userName,
                Email = email,
                EmailConfirmed = true,
                CardHolderName = $"{userName} Smith",
                CardNumber = "4012888888881881",
                CardType = 1,
                City = "Redmond",
                Country = "U.S.",
                Expiration = "12/24",
                Id = Guid.NewGuid().ToString(),
                LastName = "Smith",
                Name = userName,
                PhoneNumber = "1234567890",
                ZipCode = "98052",
                State = "WA",
                Street = "15703 NE 61st Ct",
                SecurityNumber = "123",
            };

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            var claimResult = await userManager.AddClaimAsync(user, new Claim("Role", role));
            if (!claimResult.Succeeded)
            {
                throw new Exception(claimResult.Errors.First().Description);
            }

            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug($"{userName} created with role {role}");
            }
        }
        else
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug($"{userName} already exists");
            }
        }
    }
}
