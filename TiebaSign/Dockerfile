FROM microsoft/dotnet:2.2-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY TiebaSign.csproj TiebaSign/
RUN dotnet restore TiebaSign/TiebaSign.csproj
COPY . .
RUN dotnet build TiebaSign.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TiebaSign.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TiebaSign.dll"]
