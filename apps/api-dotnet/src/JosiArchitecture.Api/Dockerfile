#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/JosiArchitecture.Api/JosiArchitecture.Api.csproj", "src/JosiArchitecture.Api/"]
COPY ["src/JosiArchitecture.Core/JosiArchitecture.Core.csproj", "src/JosiArchitecture.Core/"]
COPY ["src/JosiArchitecture.Data/JosiArchitecture.Data.csproj", "src/JosiArchitecture.Data/"]
RUN dotnet restore "src/JosiArchitecture.Api/JosiArchitecture.Api.csproj"
COPY . .
WORKDIR "/src/src/JosiArchitecture.Api"
RUN dotnet build "JosiArchitecture.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JosiArchitecture.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JosiArchitecture.Api.dll"]
