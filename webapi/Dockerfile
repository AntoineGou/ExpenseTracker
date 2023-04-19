# Set the base image as the .NET 7 SDK
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory
WORKDIR /src

# Copy the .csproj file and restore the NuGet packages
COPY ["webapi.csproj", "webapi/"]
RUN dotnet restore "webapi/webapi.csproj"

# Copy the rest of the source code
COPY . webapi/

# Build the project and publish it
WORKDIR "/src/webapi"
RUN dotnet build -c Release -o /app/publish
RUN dotnet publish "webapi.csproj" -c Release -o /app/publish

# Set the base image as the .NET 7 runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Set the working directory
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/publish .

# Expose the port that the Web API is running on
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "webapi.dll"]
