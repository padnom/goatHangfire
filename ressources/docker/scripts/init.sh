#!/bin/bash

echo "Init Db hangfire"
# Define SQL commands
SQL_COMMANDS=$(cat <<EOF
USE master;
IF NOT EXISTS (
    SELECT name
    FROM sys.databases
    WHERE name = 'hangfire'
)
BEGIN
    CREATE DATABASE hangfire;
END;
EOF
)

/opt/mssql-tools/bin/sqlcmd -S sqlserver -U SA -P Your_password123 -d master -Q "$SQL_COMMANDS"
