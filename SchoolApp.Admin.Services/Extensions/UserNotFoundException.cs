using System;

namespace SchoolApp.Admin.Services.Extensions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string userName) : base($"No user found with username: {userName}")
    {
    }
}
