#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["App.Query/App.Query.Api/App.Query.Api.csproj", "App.Query/App.Query.Api/"]
COPY ["App.Common/App.Common/App.Common.csproj", "App.Common/App.Common/"]
COPY ["CQRS-ES/CQRS.Core/CQRS.Core.csproj", "CQRS-ES/CQRS.Core/"]
COPY ["App.Query/App.Query.Domain/App.Query.Domain.csproj", "App.Query/App.Query.Domain/"]
COPY ["App.Query/App.Query.Infrastructure/App.Query.Infrastructure.csproj", "App.Query/App.Query.Infrastructure/"]
RUN dotnet restore "App.Query/App.Query.Api/App.Query.Api.csproj"
COPY . .
WORKDIR "/src/App.Query/App.Query.Api"
RUN dotnet build "App.Query.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.Query.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.Query.Api.dll"]