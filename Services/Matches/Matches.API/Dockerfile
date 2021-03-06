#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Services/Matches/Matches.API/Matches.API.csproj", "Services/Matches/Matches.API/"]
COPY ["Services/Matches/Matches.Application/Matches.Application.csproj", "Services/Matches/Matches.Application/"]
COPY ["Services/Matches/Matches.Domain/Matches.Domain.csproj", "Services/Matches/Matches.Domain/"]
COPY ["Services/Base/Base.Domain/Base.Domain.csproj", "Services/Base/Base.Domain/"]
COPY ["Services/Base/Base.Infrastructure/Base.Infrastructure.csproj", "Services/Base/Base.Infrastructure/"]
COPY ["Services/Base/Base.Application/Base.Application.csproj", "Services/Base/Base.Application/"]
COPY ["Services/Matches/Matches.Infrastructure/Matches.Infrastructure.csproj", "Services/Matches/Matches.Infrastructure/"]
RUN dotnet restore "Services/Matches/Matches.API/Matches.API.csproj"
COPY . .
WORKDIR "/src/Services/Matches/Matches.API"
RUN dotnet build "Matches.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Matches.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Matches.API.dll"]
