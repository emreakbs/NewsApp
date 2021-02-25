FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
COPY . ImageApp/
WORKDIR /ImageApp
RUN dotnet restore ImageApp.sln

FROM build AS publish
WORKDIR /ImageApp/
RUN dotnet publish ImageApp.sln -c Debug -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS runtime
COPY . ImageApp/
WORKDIR /app
EXPOSE 8003

FROM runtime AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "ImageApp.dll"]
