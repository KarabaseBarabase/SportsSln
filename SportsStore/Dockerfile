# Используем образ SDK для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем ТОЛЬКО файлы проектов для восстановления зависимостей
COPY ["SportsStore.csproj", "."]
RUN dotnet restore "SportsStore.csproj"

# Копируем весь исходный код и собираем
COPY . .
RUN dotnet build "SportsStore.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "SportsStore.csproj" -c Release -o /app/publish --no-restore

# Финальный образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 5000
ENTRYPOINT ["dotnet", "SportsStore.dll", "--urls=http://0.0.0.0:5000"]
