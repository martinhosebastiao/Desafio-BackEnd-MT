FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
LABEL author="MAS INOVACOES, LDA"
WORKDIR /app

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG configuration=Release
WORKDIR /src
COPY ["MotoBusiness.External.Presentation.API/MotoBusiness.External.Presentation.API.csproj", "MotoBusiness.External.Presentation.API/"]
RUN dotnet restore "MotoBusiness.External.Presentation.API/MotoBusiness.External.Presentation.API.csproj"
COPY . .
WORKDIR "/src/MotoBusiness.External.Presentation.API"
RUN dotnet build "MotoBusiness.External.Presentation.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "MotoBusiness.External.Presentation.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app/masinovacoes/motobusiness
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MotoBusiness.External.Presentation.API.dll"]
