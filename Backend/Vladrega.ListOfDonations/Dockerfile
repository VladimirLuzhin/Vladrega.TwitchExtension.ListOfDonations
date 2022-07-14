FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Vladrega.ListOfDonations/Vladrega.ListOfDonations.csproj", "Vladrega.ListOfDonations/"]
COPY ["Vladrega.ListOfDonations.Application/Vladrega.ListOfDonations.Application.csproj", "Vladrega.ListOfDonations.Application/"]
COPY ["Vladrega.ListOfDonations.Database/Vladrega.ListOfDonations.Database.csproj", "Vladrega.ListOfDonations.Database/"]

COPY . .
WORKDIR "/src/Vladrega.ListOfDonations"

FROM build AS publish
RUN dotnet publish "Vladrega.ListOfDonations.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vladrega.ListOfDonations.dll"]
