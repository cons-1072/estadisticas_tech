FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
        libc6-dev \
        libgdiplus \
        libx11-dev \
     && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Estadisticas/Estadisticas.csproj", "Estadisticas/"]
RUN dotnet restore "Estadisticas/Estadisticas.csproj"
COPY . .
WORKDIR "/src/Estadisticas"
RUN dotnet build "Estadisticas.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Estadisticas.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Estadisticas.dll"]