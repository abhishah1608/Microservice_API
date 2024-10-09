#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Set the environment to "Production", "Development", or "Staging".
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["API/API.csproj", "API/"]
COPY ["api.Application/api.Application.csproj", "Application/"]
COPY ["Api.domain/api.Domain.csproj", "Domain/"]
COPY ["api.Infrastructure/api.Infrastructure.csproj", "Infrastructure/"]
RUN dotnet restore "API/API.csproj"
RUN mkdir -p /app/Config
COPY . .

# Copy third-party DLLs into the container
COPY ./libs/*.dll /app/libs/

# Copy Configuration files.
COPY ./Config/* /app/Config/

WORKDIR "/src/."
RUN dotnet build "API/API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API/API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]