

using System;
using SchoolApp.AppHost;
using Microsoft.Extensions.Configuration;
using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddForwardedHeaders();
//var redis = builder.AddRedis("redis");
var rabbitMq = builder.AddRabbitMQ("eventbus").WithManagementPlugin();
var sqlserver = builder.AddSqlServer("sqlserver");
var webhooksDb = sqlserver.AddDatabase("webhooksdb");
var adminDb = sqlserver.AddDatabase("adminDb");
var identityDb = sqlserver.AddDatabase("identityDb");


var launchProfileName = ShouldUseHttpForEndpoints() ? "http" : "http";
// Services
var identityApi = builder.AddProject<Projects.SchoolApp_Identity_API>("identity-api", launchProfileName)
    .WithExternalHttpEndpoints()
    .WithReference(identityDb);
var identityEndpoint = identityApi.GetEndpoint(launchProfileName);

var adminApi = builder.AddProject<Projects.SchoolApp_Admin_WebAPI>("admin-webapi")
    .WithReference(adminDb)
    .WithReference(rabbitMq)
    .WithEnvironment("Identity__Url", identityEndpoint);

var webHooksApi = builder.AddProject<Projects.SchoolApp_Webhooks_API>("webhooks-api")
    .WithReference(rabbitMq)
    .WithReference(webhooksDb)
    .WithEnvironment("Identity__Url", identityEndpoint);

//apps
var webhooksClient = builder.AddProject<Projects.SchoolApp_Webhook_Web>("webhooksclient")
    .WithReference(webHooksApi)
    .WithEnvironment("IdentityUrl", identityEndpoint);

var adminWeb = builder.AddProject<Projects.SchoolApp_Admin_Web>("webapp", launchProfileName)
    .WithExternalHttpEndpoints()
    .WithReference(rabbitMq)
    .WithReference(adminApi)
    .WithEnvironment("IdentityUrl", identityEndpoint);



webhooksClient.WithEnvironment("CallBackUrl", webhooksClient.GetEndpoint(launchProfileName));
adminWeb.WithEnvironment("CallBackUrl", adminWeb.GetEndpoint(launchProfileName));


identityApi.WithEnvironment("AdminApiClient", adminApi.GetEndpoint("http"))
    .WithEnvironment("WebhooksApiClient", webHooksApi.GetEndpoint("http"))
    .WithEnvironment("WebhooksWebClient", webhooksClient.GetEndpoint(launchProfileName))
    .WithEnvironment("WebAppClient", adminWeb.GetEndpoint(launchProfileName));
builder.Build().Run();

static bool ShouldUseHttpForEndpoints()
{
    const string EnvVarName = "SchoolApp_USE_HTTP_ENDPOINTS";
    var envValue = Environment.GetEnvironmentVariable(EnvVarName);

    // Attempt to parse the environment variable value; return true if it's exactly "1".
    return int.TryParse(envValue, out int result) && result == 1;
}
