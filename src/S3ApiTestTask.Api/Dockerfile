#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/S3ApiTestTask.Api/S3ApiTestTask.Api.csproj", "src/S3ApiTestTask.Api/"]
COPY ["src/S3ApiTestTask.Infrastructure/S3ApiTestTask.Infrastructure.csproj", "src/S3ApiTestTask.Infrastructure/"]
COPY ["src/S3ApiTestTask.Application/S3ApiTestTask.Application.csproj", "src/S3ApiTestTask.Application/"]
COPY ["src/S3ApiTestTask.Contracts/S3ApiTestTask.Contracts.csproj", "src/S3ApiTestTask.Contracts/"]
COPY ["src/S3ApiTestTask.Domain/S3ApiTestTask.Domain.csproj", "src/S3ApiTestTask.Domain/"]
RUN dotnet restore "src/S3ApiTestTask.Api/S3ApiTestTask.Api.csproj"
COPY . .
WORKDIR "/src/src/S3ApiTestTask.Api"
RUN dotnet build "S3ApiTestTask.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "S3ApiTestTask.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "S3ApiTestTask.Api.dll"]