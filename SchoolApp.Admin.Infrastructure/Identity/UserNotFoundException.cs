﻿using System;

namespace SchoolApp.Admin.Infrastructure.Identity;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string userName) : base($"No user found with username: {userName}")
    {
    }
}