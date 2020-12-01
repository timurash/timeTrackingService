#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000/tcp

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY PL/PL.csproj PL/
COPY BLL/BLL.csproj BLL/
COPY DAL/DAL.csproj DAL/
RUN dotnet restore "PL/PL.csproj"
COPY . .
WORKDIR "/src/PL"
RUN dotnet build "PL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PL.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PL.dll"]
