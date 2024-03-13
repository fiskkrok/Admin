var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Admin_WebAPI>("adminwebapi");



builder.Build().Run();
