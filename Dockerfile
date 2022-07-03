FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["HackerNews.API/HackerNews.API.csproj", "./HackerNews.API/"]
COPY ["HackerNews.Domain/HackerNews.Domain.csproj", "./HackerNews.Domain/"]
COPY ["HackerNews.Infraestructure/HackerNews.Infraestructure.csproj", "./HackerNews.Infraestructure/"]
RUN dotnet restore "./HackerNews.API/HackerNews.API.csproj"
COPY . .
WORKDIR "/src/HackerNews.API"
RUN dotnet build "./HackerNews.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HackerNews.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HackerNews.API.dll"]