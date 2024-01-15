#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet:4 AS base
WORKDIR /home/site/wwwroot
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Reports/UBA.Panel.Report.FileProcessor.Functions/UBA.Panel.Report.FileProcessor.Functions.csproj", "Services/Reports/UBA.Panel.Report.FileProcessor.Functions/"]
COPY ["Services/Reports/UBA.Panel.Report.Domain/UBA.Panel.Report.Domain.csproj", "Services/Reports/UBA.Panel.Report.Domain/"]
COPY ["Services/Reports/UBA.Panel.Report.Common/UBA.Panel.Report.Common.csproj", "Services/Reports/UBA.Panel.Report.Common/"]
COPY ["Services/Reports/UBA.Panel.Report.Infrastructure/UBA.Panel.Report.Infrastructure.csproj", "Services/Reports/UBA.Panel.Report.Infrastructure/"]
COPY ["Services/Reports/UBA.Panel.Report.Excel/UBA.Panel.Report.Excel.csproj", "Services/Reports/UBA.Panel.Report.Excel/"]
RUN dotnet restore "./Services/Reports/UBA.Panel.Report.FileProcessor.Functions/UBA.Panel.Report.FileProcessor.Functions.csproj"
COPY . .
WORKDIR "/src/Services/Reports/UBA.Panel.Report.FileProcessor.Functions"
RUN dotnet build "./UBA.Panel.Report.FileProcessor.Functions.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UBA.Panel.Report.FileProcessor.Functions.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true