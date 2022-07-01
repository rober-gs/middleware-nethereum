FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Middelware.API/Middelware.API.csproj", "Middelware.API/"]
COPY ["Baas.Core/Baas.Core.csproj", "Baas.Core/"]
COPY ["Baas.Infrastructure/Baas.Infrastructure.csproj", "Baas.Infrastructure/"]
RUN dotnet restore "Middelware.API/Middelware.API.csproj"
COPY . .
WORKDIR "/src/Middelware.API"
RUN dotnet build "Middelware.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Middelware.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Middelware.API.dll"]
