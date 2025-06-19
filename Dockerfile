FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY JakDojadeMCP.Server ./
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:9.0-noble-chiseled AS runtime

WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "JakDojadeMCP.Server.dll"]