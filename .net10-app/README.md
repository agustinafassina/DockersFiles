### Dotnet build and run
```
dotnet build
dotnet run

# api test
http://localhost:5193/swagger/index.html
```

### Docker build and run

```
# Docker build
docker build -f Dockerfile -t api .
# Docker run in the port 8787
docker run -d -p 8080:8080 -e "ASPNETCORE_ENVIRONMENT=Development" --name api api

# Api tests
http://localhost:8080/swagger/index.html
```