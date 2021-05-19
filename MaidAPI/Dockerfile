FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["MaidAPI/Maid.Manga.API.csproj", "MaidAPI/"]
RUN dotnet restore "MaidAPI/Maid.Manga.API.csproj"
COPY . .
WORKDIR "/src/MaidAPI"
RUN dotnet build "Maid.Manga.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Maid.Manga.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maid.Manga.API.dll"]