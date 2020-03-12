#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Services/Phrases/Phrases.API/Phrases.API.csproj", "Services/Phrases/Phrases.API/"]
COPY ["Services/Phrases/Phrases.Infrastructure/Phrases.Infrastructure.csproj", "Services/Phrases/Phrases.Infrastructure/"]
COPY ["Services/Phrases/Phrases.Domain/Phrases.Domain.csproj", "Services/Phrases/Phrases.Domain/"]
COPY ["Services/Base/Base.Domain/Base.Domain.csproj", "Services/Base/Base.Domain/"]
COPY ["Services/Base/Base.Infrastructure/Base.Infrastructure.csproj", "Services/Base/Base.Infrastructure/"]
COPY ["Services/Phrases/Phrases.Application/Phrases.Application.csproj", "Services/Phrases/Phrases.Application/"]
COPY ["Services/Base/Base.Application/Base.Application.csproj", "Services/Base/Base.Application/"]
RUN dotnet restore "Services/Phrases/Phrases.API/Phrases.API.csproj"
COPY . .
WORKDIR "/src/Services/Phrases/Phrases.API"
RUN dotnet build "Phrases.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Phrases.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Phrases.API.dll"]
