# Usa la imagen base de SDK de .NET Core para compilar tu aplicación
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copia csproj y restaura las dependencias
COPY *.csproj ./
RUN dotnet restore

# Copia los archivos restantes del proyecto y construye tu aplicación
COPY . ./
RUN dotnet publish -c Release -o out

# Genera la imagen final
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HotelBookingApi.dll"]
