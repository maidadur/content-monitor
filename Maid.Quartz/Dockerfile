FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Maid.Quartz/Maid.Quartz.csproj", "Maid.Quartz/"]
RUN dotnet restore "Maid.Quartz/Maid.Quartz.csproj"
COPY . .
WORKDIR "/src/Maid.Quartz"
RUN dotnet build "Maid.Quartz.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Maid.Quartz.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Maid.Quartz.dll"]