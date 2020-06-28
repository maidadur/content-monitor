FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["MaidAPI/Maid.Manga.API.csproj", "MaidAPI/"]
RUN dotnet restore "MaidAPI/Maid.Manga.API.csproj"
COPY . .
WORKDIR "/src/MaidAPI"
RUN dotnet build "Maid.Manga.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Maid.Manga.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Maid.Manga.API.dll"]