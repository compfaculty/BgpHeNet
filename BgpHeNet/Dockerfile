FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BgpHeNet/BgpHeNet.csproj", "BgpHeNet/"]
RUN dotnet restore "BgpHeNet/BgpHeNet.csproj"
COPY . .
WORKDIR "/src/BgpHeNet"
RUN dotnet build "BgpHeNet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BgpHeNet.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BgpHeNet.dll"]
