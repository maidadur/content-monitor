FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
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