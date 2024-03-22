using GoatHangfire.ExternalJob;

using Hangfire;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ExternalGoatJob>();
builder.Services.AddScoped<IGoatExternalService, GoatExternalService>();

builder.Services.AddHangfire(configuration => configuration
                                              .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                                              .UseFilter(new AutomaticRetryAttribute { Attempts = 0, })
                                              .UseSimpleAssemblyNameTypeSerializer()
                                              .UseRecommendedSerializerSettings()
                                              .UseColouredConsoleLogProvider()
                                              .UseSqlServerStorage(
                                                  builder.Configuration.GetConnectionString("HangfireDbConnection")));

builder.Services.AddHangfireServer();
IHost host = builder.Build();
host.Run();