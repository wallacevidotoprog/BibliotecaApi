FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["BibliotecaApi.csproj", "."]
RUN dotnet restore "./BibliotecaApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "BibliotecaApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BibliotecaApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Volta temporariamente para o usuário root para criar a pasta do BD com as permissões pro usuário app
USER root
RUN mkdir -p /app/Database && chown -R app:app /app/Database
USER app

ENTRYPOINT ["dotnet", "BibliotecaApi.dll"]
