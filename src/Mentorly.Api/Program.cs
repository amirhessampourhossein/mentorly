using Asp.Versioning;
using Mentorly.Api;
using Mentorly.Api.Endpoints;
using Mentorly.Api.OpenApi;
using Mentorly.Application;
using Mentorly.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new(1);
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'V";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services
    .ConfigureApplicationLayer()
    .ConfigureInfrastructureLayer(builder.Configuration);

builder.Services
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddProblemDetails();

builder.Services.AddSwaggerGen();

builder.Services
    .AddCors(options => options
        .AddDefaultPolicy(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));

builder.Services
    .ConfigureOptions<SwaggerGenOptionsSetup>()
    .ConfigureOptions<SwaggerUIOptionsSetup>();

var app = builder.Build();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.UseExceptionHandler();

//app.RecreateDatabaseForHost();

var apiVersionSet = app
    .NewApiVersionSet()
    .HasApiVersion(new(1))
    .ReportApiVersions()
    .Build();

var versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

versionedGroup
    .MapSharedEndpoints()
    .MapAuthEndpoints()
    .MapUsersEndpoints()
    .MapToApiVersion(1);

app.Run();
