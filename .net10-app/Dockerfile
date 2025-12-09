FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy csproj and dependencies restore
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the files and publish the application
COPY . ./
RUN dotnet publish -c Release -o /release

# Use the ASP.NET base image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app
COPY --from=build /release ./

# Expose the port that the application will use
EXPOSE 80

# Run the application
ENTRYPOINT ["dotnet", ".net10-app.dll"]