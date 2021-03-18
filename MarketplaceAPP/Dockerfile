
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MarketplaceAPP/MarketplaceAPP.csproj", "MarketplaceAPP/"]
COPY ["MarketplaceAPI/MarketplaceAPI.csproj", "MarketplaceAPI/"]
RUN dotnet restore "MarketplaceAPP/MarketplaceAPP.csproj"
COPY . .
WORKDIR "/src/MarketplaceAPP"
RUN dotnet build "MarketplaceAPP.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarketplaceAPP.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarketplaceAPP.dll"]