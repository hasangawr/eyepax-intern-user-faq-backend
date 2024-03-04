#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FaqService.Api/FaqService.Api.csproj", "FaqService.Api/"]
COPY ["FaqService.Dal/FaqService.Dal.csproj", "FaqService.Dal/"]
COPY ["FaqService.Domain/FaqService.Domain.csproj", "FaqService.Domain/"]
COPY ["FaqService.Service/FaqService.Service.csproj", "FaqService.Service/"]
RUN dotnet restore "./FaqService.Api/./FaqService.Api.csproj"
COPY . .
WORKDIR "/src/FaqService.Api"
RUN dotnet build "./FaqService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FaqService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FaqService.Api.dll"]