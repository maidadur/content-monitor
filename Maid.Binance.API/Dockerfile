FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Maid.Binance.API/Maid.Binance.API.csproj", "Maid.Binance.API/"]
RUN dotnet restore "./Maid.Binance.API/./Maid.Binance.API.csproj"
COPY . .
WORKDIR "/src/Maid.Binance.API"
RUN dotnet build "./Maid.Binance.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Maid.Binance.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maid.Binance.API.dll"]