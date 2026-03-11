# BUILD STAGE

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set working directory
WORKDIR /src

COPY ["src/ModularAuth.WebAPI/ModularAuth.WebAPI.csproj", "src/ModularAuth.WebAPI/"] 
COPY ["src/ModularAuth.Application/ModularAuth.Application.csproj", "src/ModularAuth.Application/"]
COPY ["src/ModularAuth.Domain/ModularAuth.Domain.csproj", "src/ModularAuth.Domain/"]
COPY ["src/ModularAuth.Infrastructure/ModularAuth.Infrastructure.csproj", "src/ModularAuth.Infrastructure/"]



# Restor the project
RUN dotnet restore "src/ModularAuth.WebAPI/ModularAuth.WebAPI.csproj"


# Copy all other project files

COPY . .



RUN dotnet build "src/ModularAuth.WebAPI/ModularAuth.WebAPI.csproj" -c Release -o /app/build



# publish stage

FROM build AS publish

RUN dotnet publish "src/ModularAuth.WebAPI/ModularAuth.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false


# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

EXPOSE 8080

COPY --from=publish /app/publish .

RUN adduser --disabled-password --gecos '' appuser

RUN chown -R appuser:appuser /app

USER appuser

ENTRYPOINT [ "dotnet", "ModularAuth.WebAPI.dll" ]