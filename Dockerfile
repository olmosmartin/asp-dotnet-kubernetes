FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["asp-dotnet-kubernetes.csproj", "./"]
RUN dotnet restore "asp-dotnet-kubernetes.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "asp-dotnet-kubernetes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "asp-dotnet-kubernetes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "asp-dotnet-kubernetes.dll"]
