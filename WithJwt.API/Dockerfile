#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WithJwt.API/WithJwt.API.csproj", "WithJwt.API/"]
COPY ["WithJwt.Service/WithJwt.Service.csproj", "WithJwt.Service/"]
COPY ["WithJwt.Repository/WithJwt.Repository.csproj", "WithJwt.Repository/"]
COPY ["WithJwt.Core/WithJwt.Core.csproj", "WithJwt.Core/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "WithJwt.API/WithJwt.API.csproj"
COPY . .
WORKDIR "/src/WithJwt.API"
RUN dotnet build "WithJwt.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WithJwt.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WithJwt.API.dll"]