#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["App.Cmd/App.Cmd.Api/App.Cmd.Api.csproj", "App.Cmd/App.Cmd.Api/"]
COPY ["CQRS-ES/CQRS.Core/CQRS.Core.csproj", "CQRS-ES/CQRS.Core/"]
COPY ["App.Cmd/App.Cmd.Domain/App.Cmd.Domain.csproj", "App.Cmd/App.Cmd.Domain/"]
COPY ["App.Common/App.Common/App.Common.csproj", "App.Common/App.Common/"]
COPY ["App.Cmd/App.Cmd.Infrastructure/App.Cmd.Infrastructure.csproj", "App.Cmd/App.Cmd.Infrastructure/"]
RUN dotnet restore "App.Cmd/App.Cmd.Api/App.Cmd.Api.csproj"
COPY . .
WORKDIR "/src/App.Cmd/App.Cmd.Api"
RUN dotnet build "App.Cmd.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "App.Cmd.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.Cmd.Api.dll"]