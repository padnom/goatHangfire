#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/GoatHangfire.Dashboard/GoatHangfire.Dashboard.csproj", "src/GoatHangfire.Dashboard/"]
COPY ["src/GoatHangfire.InternalJob/GoatHangfire.InternalJob.csproj", "src/GoatHangfire.InternalJob/"]
RUN dotnet restore "./src/GoatHangfire.Dashboard/GoatHangfire.Dashboard.csproj"
COPY . .
WORKDIR "/src/src/GoatHangfire.Dashboard"
RUN dotnet build "./GoatHangfire.Dashboard.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GoatHangfire.Dashboard.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoatHangfire.Dashboard.dll"]