FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
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