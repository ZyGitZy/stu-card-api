#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["stu-card-api/stu-card-api.csproj", "stu-card-api/"]
COPY ["stu-card-entity-store/stu-card-entity-store.csproj", "stu-card-entity-store/"]
RUN dotnet restore "stu-card-api/stu-card-api.csproj"
COPY . .
WORKDIR "/src/stu-card-api"
RUN dotnet build "stu-card-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "stu-card-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "stu-card-api.dll"]