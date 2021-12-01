FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["reserreadingbe/reserreadingbe.csproj", "reserreadingbe/"]
RUN dotnet restore "reserreadingbe/reserreadingbe.csproj"
COPY . .
WORKDIR "/src/reserreadingbe"
RUN dotnet build "reserreadingbe.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "reserreadingbe.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "reserreadingbe.dll"]
