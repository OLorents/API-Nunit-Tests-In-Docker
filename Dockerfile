# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY WebApp/*.csproj ./webapp/
RUN dotnet restore

# copy everything else and build app
COPY webapp/. ./webapp/
WORKDIR /source/webapp
RUN dotnet publish -c release -o /app 

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "webapp.dll"]
