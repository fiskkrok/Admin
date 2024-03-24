using SchoolApp.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddForwardedHeaders();
var redis = builder.AddRedis("redis");
var rabbitMq = builder.AddRabbitMQ("eventbus");
var sqlserver = builder.AddSqlServer("sqlserver");

var adminDb = sqlserver.AddDatabase("adminDb");
var identityDb = sqlserver.AddDatabase("identityDb");
var webhooksDb = sqlserver.AddDatabase("webhooksdb");

// Services

var adminApi = builder.AddProject<Projects.SchoolApp_Admin_WebAPI>("admin-webapi")
    .WithReference(adminDb)
    .WithReference(identityDb)
    .WithLaunchProfile("https"); 

var idpHttps = adminApi.GetEndpoint("https");
var adminWeb = builder.AddProject<Projects.SchoolApp_Admin_Web>("admin-web")
    .WithReference(adminApi)
    .WithReference(rabbitMq)
    //.WithEnvironment("IdentityUrl", idpHttps)
    .WithLaunchProfile("https");

var webHooksApi = builder.AddProject<Projects.SchoolApp_Webhooks_API>("webhooks-api")
    .WithReference(rabbitMq)
    .WithReference(webhooksDb)
    .WithEnvironment("Identity__Url", idpHttps);
var webhooksClient = builder.AddProject<Projects.SchoolApp_Webhook_Web>("webhooks-web")
    .WithReference(webHooksApi)
    .WithEnvironment("IdentityUrl", idpHttps);

// Wire up the callback urls (self referencing)
adminWeb.WithEnvironment("CallBackUrl", adminWeb.GetEndpoint("https"));
webhooksClient.WithEnvironment("CallBackUrl", webhooksClient.GetEndpoint("http"));

//Identity has a reference to all of the apps for callback urls, this is a cyclic reference
adminApi.WithEnvironment("AdminWebAPI", adminApi.GetEndpoint("http"))
    .WithEnvironment("WebhooksApiClient", webHooksApi.GetEndpoint("http"))
    .WithEnvironment("WebhooksWebClient", webhooksClient.GetEndpoint("http"));

builder.Build().Run();
