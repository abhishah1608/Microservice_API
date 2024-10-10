# Base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 443

# Set environment variables for SSL and HTTPS
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=https://+:443
ENV ASPNETCORE_HTTPS_PORT=443

# SDK image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy project files for restore
COPY ["API/API.csproj", "API/"]
COPY ["api.Application/api.Application.csproj", "Application/"]
COPY ["Api.domain/api.Domain.csproj", "Domain/"]
COPY ["api.Infrastructure/api.Infrastructure.csproj", "Infrastructure/"]

# Restore dependencies
RUN dotnet restore "API/API.csproj"

# Copy everything else in one step to preserve context
COPY . .

# Create directories and copy libraries, certs, and config at once
RUN mkdir -p /app/Config /app/libs /app/Certs && \
    cp -R ./libs/*.dll /app/libs/ && \
    cp -R API/Certs/mycert.key /app/Certs/ && \
    cp -R API/Certs/mycert.crt /app/Certs/ && \
    cp -R ./Config/* /app/Config/

# Build the application
WORKDIR "/src/."
RUN dotnet build "API/API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "API/API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image for running the application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "API.dll"]
