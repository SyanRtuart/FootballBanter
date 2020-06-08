#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Services/UserAccess/UserAccess.API/UserAccess.API/UserAccess.API.csproj", "Services/UserAccess/UserAccess.API/UserAccess.API/"]
COPY ["Services/UserAccess/UserAccess.Application/UserAccess.Application.csproj", "Services/UserAccess/UserAccess.Application/"]
COPY ["Services/UserAccess/UserAccess.Infrastructure/UserAccess.Infrastructure.csproj", "Services/UserAccess/UserAccess.Infrastructure/"]
COPY ["Services/UserAccess/UserAccess.Domain/UserAccess.Domain.csproj", "Services/UserAccess/UserAccess.Domain/"]
COPY ["Services/Base/Base.Domain/Base.Domain.csproj", "Services/Base/Base.Domain/"]
COPY ["Services/Base/Base.Infrastructure/Base.Infrastructure.csproj", "Services/Base/Base.Infrastructure/"]
COPY ["Services/Base/Base.Application/Base.Application.csproj", "Services/Base/Base.Application/"]
RUN dotnet restore "Services/UserAccess/UserAccess.API/UserAccess.API/UserAccess.API.csproj"
COPY . .
WORKDIR "/src/Services/UserAccess/UserAccess.API/UserAccess.API"
RUN dotnet build "UserAccess.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserAccess.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserAccess.API.dll"]