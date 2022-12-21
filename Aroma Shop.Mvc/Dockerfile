#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Aroma Shop.Mvc/Aroma Shop.Mvc.csproj", "Aroma Shop.Mvc/"]
COPY ["Aroma Shop.Application/Aroma Shop.Application.csproj", "Aroma Shop.Application/"]
COPY ["Aroma Shop.Domain/Aroma Shop.Domain.csproj", "Aroma Shop.Domain/"]
COPY ["Aroma Shop.Ioc/Aroma Shop.Ioc.csproj", "Aroma Shop.Ioc/"]
COPY ["Aroma Shop.Data/Aroma Shop.Data.csproj", "Aroma Shop.Data/"]
RUN dotnet restore "Aroma Shop.Mvc/Aroma Shop.Mvc.csproj"
COPY . .
WORKDIR "/src/Aroma Shop.Mvc"
RUN dotnet build "Aroma Shop.Mvc.csproj" -o /app/build

FROM build AS publish
RUN dotnet publish "Aroma Shop.Mvc.csproj" -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aroma Shop.Mvc.dll"]