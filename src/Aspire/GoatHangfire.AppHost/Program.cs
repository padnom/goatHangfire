IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);
IResourceBuilder<SqlServerDatabaseResource> hangfireDb = builder.AddSqlServer("sqlserver", "Your_password123", 1633)
    // Mount the init scripts directory into the container.
    .WithBindMount("../../../ressources/aspire/scripts", "/usr/config")
    // Mount the SQL scripts directory into the container so that the init scripts run.
    .WithBindMount("../../../ressources/aspire/scripts", "/docker-entrypoint-initdb.d")
    // Run the custom entrypoint script on startup.
    .WithEntrypoint("/usr/config/entrypoint.sh")
    // Add the database to the application model so that it can be referenced by other resources.
    .WithBindMount("../../../ressources/aspire/volumes", "/var/opt/mssql/data")
    .AddDatabase("hangfire");

builder.AddProject<Projects.GoatHangfire_Dashboard>("goathangfire-dashboard")
       .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Aspire")
       .WithReference(hangfireDb);

builder.AddProject<Projects.GoatHangfire_ExternalJob>("goathangfire-externaljob")
       .WithEnvironment("ASPNETCORE_ENVIRONMENT", "Aspire")
       .WithReference(hangfireDb);

builder.Build().Run();
