
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 85
EXPOSE 447

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MarketplaceAPI/MarketplaceAPI.csproj", "MarketplaceAPI/"]
RUN dotnet restore "MarketplaceAPI/MarketplaceAPI.csproj"
COPY . .
WORKDIR "/src/MarketplaceAPI"
RUN dotnet build "MarketplaceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarketplaceAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarketplaceAPI.dll"]