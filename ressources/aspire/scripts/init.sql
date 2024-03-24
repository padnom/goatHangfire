IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'hangfire')
BEGIN
  CREATE DATABASE hangfire;
END;
GO