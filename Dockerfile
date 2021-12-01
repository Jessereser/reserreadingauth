FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["reserreadingauth/reserreadingauth.csproj", "reserreadingauth/"]
RUN dotnet restore "reserreadingauth/reserreadingauth.csproj"
COPY . .
WORKDIR "/src/reserreadingauth"
RUN dotnet build "reserreadingauth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "reserreadingauth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "reserreadingauth.dll"]
