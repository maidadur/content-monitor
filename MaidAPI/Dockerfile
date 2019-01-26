FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["MaidAPI.csproj", "MaidAPI/"]
RUN dotnet restore "MaidAPI/MaidAPI.csproj"
COPY . MaidAPI/
WORKDIR "/src/MaidAPI"
RUN dotnet build "MaidAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MaidAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MaidAPI.dll"]