#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TiendaServicios.API.Libro/TiendaServicios.API.Libro.csproj", "TiendaServicios.API.Libro/"]
RUN dotnet restore "TiendaServicios.API.Libro/TiendaServicios.API.Libro.csproj"
COPY . .
WORKDIR "/src/TiendaServicios.API.Libro"
RUN dotnet build "TiendaServicios.API.Libro.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TiendaServicios.API.Libro.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TiendaServicios.API.Libro.dll"]
