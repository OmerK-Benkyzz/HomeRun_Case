FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Proje dosyalarını kopyalayın ve bağımlılıkları yükleyin
COPY ["Rating.Api/Rating.Api.csproj", "Rating.Api/"]
COPY ["Core.Application/Core.Application.csproj", "Core.Application/"]
COPY ["Core.Domain/Core.Domain.csproj", "Core.Domain/"]
COPY ["Rating.Domain/Rating.Domain.csproj", "Rating.Domain/"]
COPY ["Rating.Application/Rating.Application.csproj", "Rating.Application/"]
COPY ["Core.Persistance/Core.Persistance.csproj", "Core.Persistance/"]
COPY ["Rating.Persistance/Rating.Persistance.csproj", "Rating.Persistance/"]
RUN dotnet restore "Rating.Api/Rating.Api.csproj"

# Geri kalan kaynak kodunu kopyalayın ve uygulamayı derleyin
COPY . .
WORKDIR "/src/Rating.Api"
RUN dotnet build "Rating.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rating.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rating.Api.dll"]
