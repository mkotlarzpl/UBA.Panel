FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Reports/UBA.Panel.Report.Api/UBA.Panel.Report.Api.csproj", "Services/Reports/UBA.Panel.Report.Api/"]
RUN dotnet restore "Services/Reports/UBA.Panel.Report.Api/UBA.Panel.Report.Api.csproj"
COPY . .
WORKDIR "/src/Services/Reports/UBA.Panel.Report.Api"
RUN dotnet build "UBA.Panel.Report.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UBA.Panel.Report.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UBA.Panel.Report.Api.dll"]
