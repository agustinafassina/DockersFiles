# TemplateApi with .Net8
This repository provides a REST API template built with .NET 8, designed to streamline and accelerate the deployment of web services in your daily projects. It includes built-in authorization to secure your endpoints from the start, enabling quick and secure deployment. Perfect for developers seeking a solid, easy-to-configure base for their APIs.

### Structure
└─ .gitignore <br>
└─ ./Services <br>
└─ ./Unitest <br>
└─ ./Controllers <br>
└─ Dockerfile <br>
└─ README.md <br>

### How to use this repository?
```
dotner run
```

## Docker Build

```
docker build -f Dockerfile -t api .
docker run -d -p 3434:80 -e "ASPNETCORE_ENVIRONMENT=Development" --name api api
# test api:
http://localhost:3434/swagger/index.html
```

