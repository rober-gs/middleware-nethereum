FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 5001
EXPOSE 5000

ENV ASPNETCORE_URLS=https://+:5001,http://+:5000

ENV URL_API=https://vault.dev.devdicio.net:8200/v1/
ENV TOKEN_API=s.kGnVB0j4l7A9pAIdElkahQkI

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["Middelware.API/Middelware.API.csproj", "Middelware.API/"]
RUN dotnet restore "Middelware.API/Middelware.API.csproj"
COPY . .
WORKDIR "/src/Middelware.API"
RUN dotnet build "Middelware.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Middelware.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Middelware.API.dll"]