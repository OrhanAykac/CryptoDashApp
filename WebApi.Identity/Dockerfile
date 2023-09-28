
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 7004

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebApi.Identity/WebApi.Identity.csproj", "WebApi.Identity/"]
COPY ["Business/Business.csproj", "Business/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["Entities/Entities.csproj", "Entities/"]
COPY ["ExternalServices/ExternalServices.csproj", "ExternalServices/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "WebApi.Identity/WebApi.Identity.csproj"
COPY . .
WORKDIR "/src/WebApi.Identity"
RUN dotnet build "WebApi.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.Identity.dll"]