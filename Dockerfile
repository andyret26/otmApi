#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY "otmApi.csproj" .
RUN dotnet restore "./otmApi.csproj"
COPY . .
RUN dotnet build "otmApi.csproj" -c Release -o /app/build

FROM build AS publish
WORKDIR /app

RUN dotnet publish "otmApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "otmApi.dll"]