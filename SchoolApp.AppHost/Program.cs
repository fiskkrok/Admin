using SchoolApp.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddForwardedHeaders();
var redis = builder.AddRedis("redis");
var rabbitMq = builder.AddRabbitMQ("eventbus");
var sqlserver = builder.AddSqlServer("sqlserver");

var adminDb = sqlserver.AddDatabase("admindb");

//var identityDb = sqlserver.AddDatabase("identitydb");
//var webhooksDb = sqlserver.AddDatabase("webhooksdb");

// Services

var adminApi = builder.AddProject<Projects.SchoolApp_Admin_WebAPI>("admin-webapi")
    .WithReference(adminDb)
    .WithLaunchProfile("https"); 

var idpHttps = adminApi.GetEndpoint("https");

//var webHooksApi = builder.AddProject<Projects.Webhooks_API>("webhooks-api")
//    .WithReference(rabbitMq)
//    .WithReference(webhooksDb)
//    .WithEnvironment("Identity__Url", idpHttps);

var adminWeb = builder.AddProject<Projects.SchoolApp_Admin_Web>("admin-web")
    .WithReference(adminApi)
    .WithReference(rabbitMq)
    //.WithEnvironment("IdentityUrl", idpHttps)
    .WithLaunchProfile("https");

// Wire up the callback urls (self referencing)
adminWeb.WithEnvironment("CallBackUrl", adminWeb.GetEndpoint("https"));
//webhooksClient.WithEnvironment("CallBackUrl", webhooksClient.GetEndpoint("https"));

// Identity has a reference to all of the apps for callback urls, this is a cyclic reference
//identityApi.WithEnvironment("BasketApiClient", basketApi.GetEndpoint("http"))
//           .WithEnvironment("OrderingApiClient", orderingApi.GetEndpoint("http"))
//           .WithEnvironment("WebhooksApiClient", webHooksApi.GetEndpoint("http"))
//           .WithEnvironment("WebhooksWebClient", webhooksClient.GetEndpoint("https"))
//           .WithEnvironment("WebAppClient", webApp.GetEndpoint("https"));

builder.Build().Run();
