FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Basket.API/Basket.API.csproj", "Services/Basket.API/"]
COPY ["BuildingBlocks/Common.Logging/Common.Logging.csproj", "BuildingBlocks/Common.Logging/"]
COPY ["BuildingBlocks/Contracts/Contracts.csproj", "BuildingBlocks/Contracts/"]
COPY ["BuildingBlocks/Infrastructure/Infrastructure.csproj", "BuildingBlocks/Infrastructure/"]
COPY ["BuildingBlocks/EventBus/EventBus.Messages/EventBus.Messages.csproj", "BuildingBlocks/EventBus/EventBus.Messages/"]

RUN dotnet restore "Services/Basket.API/Basket.API.csproj"
COPY . .
WORKDIR "/src/Services/Basket.API"
RUN dotnet build "./Basket.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Basket.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.API.dll"]