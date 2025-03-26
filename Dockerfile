# Gebruik een .NET SDK afbeelding voor het bouwen van je project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Zet de werkmap in de container
WORKDIR /app

# Kopieer csproj-bestanden en herstel de dependencies
COPY *.csproj ./
RUN dotnet restore

# Kopieer de rest van de bestanden en bouw de applicatie
COPY . ./
RUN dotnet publish -c Release -o /out

# Gebruik een runtime-afbeelding voor de productie
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

# Zet de werkmap in de container
WORKDIR /app

# Kopieer de gepubliceerde bestanden van de build fase
COPY --from=build /out .

# Expose poort 8080
EXPOSE 8080

# Start de applicatie
ENTRYPOINT ["dotnet", "foto-backend.dll"]
