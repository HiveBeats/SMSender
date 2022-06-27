FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 5000

# build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./SMSender.Api/SMSender.Api.csproj", "./SMSender.Api/"]
COPY ["./SMSender.Shared/SMSender.Shared.csproj", "./SMSender.Shared/"]
RUN dotnet restore "./SMSender.Api/SMSender.Api.csproj"
RUN dotnet restore "./SMSender.Shared/SMSender.Shared.csproj"
COPY ./SMSender.Api/ ./SMSender.Api/
COPY ./SMSender.Shared/ ./SMSender.Shared/

# publish
FROM build AS publish
RUN dotnet publish "./SMSender.Api/SMSender.Api.csproj" -c Release -o /app/publish

# prod
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ./SMSender.Api/wait-for.sh .
RUN ["chmod", "+x", "./wait-for.sh"]
ENTRYPOINT ["dotnet", "SMSender.Api.dll"]
