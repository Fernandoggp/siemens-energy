# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./
WORKDIR /app/src/Project.Api
RUN dotnet publish -c Release -o /out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Define a porta escutada pela aplicação
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /out ./
ENTRYPOINT ["dotnet", "Project.Api.dll"]