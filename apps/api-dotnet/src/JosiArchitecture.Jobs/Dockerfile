FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/JosiArchitecture.Jobs/JosiArchitecture.Jobs.csproj", "src/JosiArchitecture.Jobs/"]
RUN dotnet restore "src/JosiArchitecture.Jobs/JosiArchitecture.Jobs.csproj"
COPY . .
WORKDIR "/src/src/JosiArchitecture.Jobs"
RUN dotnet build "JosiArchitecture.Jobs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JosiArchitecture.Jobs.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JosiArchitecture.Jobs.dll"]
