IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.GoatHangfire_Dashboard>("GoatHangfireDashboard");

builder.Build().Run();
