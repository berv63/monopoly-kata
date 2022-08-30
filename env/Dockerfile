FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
RUN apt-get update && apt-get install -y locales && locale-gen

ENV LANG en_US.UTF-8
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT false

FROM mcr.microsoft.com/dotnet/sdk:5.0.202 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
WORKDIR "/src/src/Monopoly.Web"
RUN dotnet publish "Monopoly.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Monopoly.Web.dll"]