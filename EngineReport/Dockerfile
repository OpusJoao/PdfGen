# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the application code
COPY . ./

ENV PATH="${PATH}:/root/.dotnet/tools"

# Build the application
RUN dotnet publish -c Release -o out

# Stage 2: Run Migrations and Create the Final Image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

# Copy the published application
COPY --from=build-env /app/out .

RUN mkdir Temp

# Set the entry point to start the application
ENTRYPOINT ["dotnet", "EngineReport.dll"]