FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Maid.Content.API/Maid.Content.API.csproj", "Maid.Content.API/"]
RUN dotnet restore "Maid.Content.API/Maid.Content.API.csproj"
COPY . .
WORKDIR "/src/Maid.Content.API"
RUN dotnet build "Maid.Content.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Maid.Content.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maid.Content.API.dll"]