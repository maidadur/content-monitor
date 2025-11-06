#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Maid.Storage.API/Maid.Storage.API.csproj", "Maid.Storage.API/"]
RUN dotnet restore "Maid.Storage.API/Maid.Storage.API.csproj"
COPY . .
WORKDIR "/src/Maid.Storage.API"
RUN dotnet build "Maid.Storage.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Maid.Storage.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maid.Storage.API.dll"]