FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Proje dosyalarını kopyalayın ve bağımlılıkları yükleyin
COPY ["../Core.Application/Core.Application.csproj", "Core.Application/"]
COPY ["../Core.Domain/Core.Domain.csproj", "Core.Domain/"]
COPY ["../Core.Persistance/Core.Persistance.csproj", "Core.Persistance/"]
COPY ["../Notification.Api/Notification.Api.csproj", "Notification.Api/"]
COPY ["../Notification.Application/Notification.Application.csproj", "Notification.Application/"]
COPY ["../Notification.Domain/Notification.Domain.csproj", "Notification.Domain/"]
COPY ["../Notification.Persistance/Notification.Persistance.csproj", "Notification.Persistance/"]
RUN dotnet restore "Notification.Api/Notification.Api.csproj"

# Geri kalan kaynak kodunu kopyalayın ve uygulamayı derleyin
COPY .. .
WORKDIR "/src/Notification.Api"
RUN dotnet build "Notification.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notification.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.Api.dll"]
