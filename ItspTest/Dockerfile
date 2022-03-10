FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY ["ItspTest.Api/ItspTest.Api.csproj", "ItspTest.Api/"]
COPY ["ItspTest.Core/ItspTest.Core.csproj", "ItspTest.Core/"]
RUN dotnet restore "ItspTest.Api/ItspTest.Api.csproj"

COPY . ./
WORKDIR "/app/ItspTest.Api"
RUN dotnet publish "ItspTest.Api.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
EXPOSE 5000
COPY --from=build-env /app/ItspTest.Api/out .
ENTRYPOINT ["dotnet","ItspTest.Api.dll"]

# docker build -t iptest/api:latest .
# docker run -d -p 5000:5000 iptest/api:latest