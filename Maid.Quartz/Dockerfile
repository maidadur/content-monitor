FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
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