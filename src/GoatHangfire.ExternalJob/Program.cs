using GoatHangfire.ExternalJob;

using Hangfire;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ExternalGoatJob>();
builder.Services.AddSingleton<IGoatExternalService, GoatExternalService>();
Thread.Sleep(millisecondsTimeout: 10000);
builder.Services.AddHangfire(configuration => configuration
                                              .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                                              .UseFilter(new AutomaticRetryAttribute { Attempts = 0, })
                                              .UseSimpleAssemblyNameTypeSerializer()
                                              .UseRecommendedSerializerSettings()
                                              .UseColouredConsoleLogProvider()
                                              .UseSqlServerStorage(
                                                  builder.Configuration.GetConnectionString("hangfire")));

builder.Services.AddHangfireServer();
IHost host = builder.Build();
host.Run();