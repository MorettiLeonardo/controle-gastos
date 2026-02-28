# -------- BUILD --------
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

COPY . .

RUN dotnet restore src/API/ControleGastos.Api/ControleGastos.Api.csproj
RUN dotnet publish src/API/ControleGastos.Api/ControleGastos.Api.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "ControleGastos.Api.dll"]