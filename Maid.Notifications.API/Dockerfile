FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Maid.Notifications.API/Maid.Notifications.API.csproj", "Maid.Notifications.API/"]
RUN dotnet restore "Maid.Notifications.API/Maid.Notifications.API.csproj"
COPY . .
WORKDIR "/src/Maid.Notifications.API"
RUN dotnet build "Maid.Notifications.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Maid.Notifications.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maid.Notifications.API.dll"]