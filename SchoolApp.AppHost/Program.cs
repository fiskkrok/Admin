using SchoolApp.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddForwardedHeaders();
//var redis = builder.AddRedis("redis");
//    .WithReference(redis)
var rabbitMq = builder.AddRabbitMQ("eventbus");
var sqlserver = builder.AddSqlServer("sqlserver");

var adminDb = sqlserver.AddDatabase("adminDb");
var identityDb = sqlserver.AddDatabase("identityDb");
var webhooksDb = sqlserver.AddDatabase("webhooksdb");

// Services

var adminApi = builder.AddProject<Projects.SchoolApp_Admin_WebAPI>("admin-webapi")
    .WithReference(adminDb)
    .WithReference(identityDb)
    .WithLaunchProfile("http");
var webHooksApi = builder.AddProject<Projects.SchoolApp_Webhooks_API>("webhooks-api")
    .WithReference(rabbitMq)
    .WithReference(webhooksDb)
    .WithLaunchProfile("http");
var webhooksClient = builder.AddProject<Projects.SchoolApp_Webhook_Web>("webhooksclient")
    .WithReference(webHooksApi)
    .WithLaunchProfile("https");
var adminWeb = builder.AddProject<Projects.SchoolApp_Admin_Web>("admin-web")
    .WithReference(rabbitMq)
    .WithReference(adminApi)
    .WithLaunchProfile("https");
webhooksClient.WithEnvironment("CallBackUrl", webhooksClient.GetEndpoint("https"));
adminWeb.WithEnvironment("CallBackUrl", adminWeb.GetEndpoint("https"));


builder.Build().Run();

