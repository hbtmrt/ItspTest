FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ItspTest.Api/ItspTest.Api.csproj", "ItspTest.Api/"]
COPY ["ItspTest.Core/ItspTest.Core.csproj", "ItspTest.Core/"]
RUN dotnet restore "ItspTest.Api/ItspTest.Api.csproj"
COPY . .
WORKDIR "/src/ItspTest.Api"
RUN dotnet build "ItspTest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ItspTest.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ItspTest.Api.dll"]
