FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build-env
WORKDIR /app

COPY ./powerplant-challenge/src ./src
COPY ./powerplant-challenge/test ./test
COPY ./powerplant-challenge/powerplant-challenge.sln .
RUN dotnet restore
RUN dotnet publish -c Release -o out powerplant-challenge.sln
RUN dotnet test test/Domain.Services.Tests/bin/Release/netcoreapp3.1/Domain.Services.Tests.dll

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Presentation.WebAPI.dll"]