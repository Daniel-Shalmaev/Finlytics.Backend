FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# העתק את כל קבצי הפתרון
COPY . .

# שחזור תלויות
RUN dotnet restore "Finlytics.Backend.sln"

# בנייה
RUN dotnet publish "Finlytics.Presentation/Finlytics.Presentation.csproj" -c Release -o /app/publish

# שלב הריצה
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Finlytics.Presentation.dll"]
