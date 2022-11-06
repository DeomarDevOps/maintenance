#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY MaintenanceCheckinCheckout.sln ./
COPY ["MaintenanceCheckinCheckout.API/MaintenanceCheckinCheckout.API.csproj", "MaintenanceCheckinCheckout.API/"]
COPY ["MaintenanceCheckinCheckout.Application/MaintenanceCheckinCheckout.Application.csproj", "MaintenanceCheckinCheckout.Application/"]
COPY ["MaintenanceCheckinCheckout.Domain/MaintenanceCheckinCheckout.Domain.csproj", "MaintenanceCheckinCheckout.Domain/"]
COPY ["MaintenanceCheckinCheckout.Infra.IoC/MaintenanceCheckinCheckout.Infra.IoC.csproj", "MaintenanceCheckinCheckout.Infra.IoC/"]
COPY ["MaintenanceCheckinCheckout.Infra/MaintenanceCheckinCheckout.Infra.csproj", "MaintenanceCheckinCheckout.Infra/"]
RUN dotnet restore "MaintenanceCheckinCheckout.API/MaintenanceCheckinCheckout.API.csproj"
COPY . .
WORKDIR "/src/MaintenanceCheckinCheckout.API"
RUN dotnet build "MaintenanceCheckinCheckout.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MaintenanceCheckinCheckout.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MaintenanceCheckinCheckout.API.dll"]