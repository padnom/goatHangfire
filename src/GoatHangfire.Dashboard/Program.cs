using GoatHangfire.Dashboard.DashboardAuthorizationFilters;
using GoatHangfire.InternalJob;

using Hangfire;

using Microsoft.AspNetCore.Mvc;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGoatService, GoatService>();
builder.Services.AddScoped<InternalGoatJob>();
builder.AddSqlServerClient("hangfire");

builder.Services.AddHangfire(configuration => configuration
                                              .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                                              .UseFilter(new AutomaticRetryAttribute { Attempts = 0, })
                                              .UseSimpleAssemblyNameTypeSerializer()
                                              .UseRecommendedSerializerSettings()
                                              .UseColouredConsoleLogProvider()
                                              .UseSqlServerStorage(
                                                builder.Configuration.GetConnectionString("Hangfire")));
builder.Services.AddHangfireServer();

WebApplication app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseHangfireDashboard("/dashboard",
                         new DashboardOptions { Authorization = new[] { new AuthorizationAlwayTrueFilter(), }, });

app.MapPost("/hangfire/create-queue-jobAsync",
             (InternalGoatJob internalGoatJob) =>
            {
                internalGoatJob.ExecuteQueueJobAsync(CancellationToken.None);
            });

app.MapPost("/hangfire/create-queue-job",
            (InternalGoatJob internalGoatJob) =>
            {
                internalGoatJob.ExecuteQueueJob();
            });

app.MapPost("/hangfire/createupdate-recurring-job",
            async (InternalGoatJob internalGoatJob, [FromBody] string cronExpression) =>
            {
                await internalGoatJob.ExecuteRecurringJobAsync(cronExpression, CancellationToken.None);
            });

if (app.Environment.IsDevelopment()
    || app.Environment.EnvironmentName.Contains("Docker")
    || app.Environment.EnvironmentName.Contains("Aspire"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();