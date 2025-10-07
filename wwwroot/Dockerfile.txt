# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /out

# Final
FROM base AS final
WORKDIR /app
COPY --from=build /out .
COPY App_Data ./App_Data
ENTRYPOINT ["dotnet", "LodSalgsSystemFDF.dll"]
