var builder = DistributedApplication.CreateBuilder(args);
//var redis = builder.AddRedisContainer("redis");
//var rabbitMq = builder.AddRabbitMQContainer("eventbus");
//var sqlServer = builder.AddSqlServer(
//var AdminDbContext = sqlServer.AddDatabase("AdminDbContext");
builder.AddProject<Projects.SchoolApp_Admin_WebAPI>("admin-webapi");
builder.AddProject<Projects.SchoolApp_Admin_Web>("admin-web");

builder.Build().Run();
