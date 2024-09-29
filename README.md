## DDT NetCore Microservices:

## Environment

* Install dotnet core version in file `global.json`
* Visual Studio 2022+
* Docker desktop

---
## How to run the project

Run command for build project
```Powershell
dotnet build
```

Go to folder contain file `docker-compose`

1. Using docker-compose
```Powershell
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans
```

## Application URLs - LOCAL Environment (Docker container):
- Product API: http://localhost:6002/api/products

## Docker Application URLs - LOCAL Environment (Docker container):
- Portainer: http://localhost:9000 - username: admin; pass: 123123123123
- Kibana: http://localhost:5601 - username: elestic; pass: admin
- RabbitMQ: http://localhost:15672 - username: guest; pass: guest

2. Using Visual Studio 2022
- Open aspnetcore-microservices.sln - `aspnetcore-microservices.sln`
- Run compound to start muti project
---
## Application URLs - DEVELOPMENT Environment:
- Product API: http://localhost:5002/api/products


---
## Docker Application URLs - PRODUCTION Environment:

---
## Packages References

## Install Environment

- https://dotnet.microsoft.com/download/dotnet/8.0
- https://visualstudio.microsoft.com/

## References URLs

## Docker Commands:

- docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans --build

