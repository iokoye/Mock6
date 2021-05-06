#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Mock6/Mock6.csproj", "Mock6/"]
RUN dotnet restore "Mock6/Mock6.csproj"
COPY . .
WORKDIR "/src/Mock6"
RUN dotnet build "Mock6.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mock6.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mock6.dll"]