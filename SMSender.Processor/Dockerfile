FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 5000

# build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./SMSender.Processor/SMSender.Processor.csproj", "./SMSender.Processor/"]
COPY ["./SMSender.Shared/SMSender.Shared.csproj", "./SMSender.Shared/"]
RUN dotnet restore "./SMSender.Processor/SMSender.Processor.csproj"
RUN dotnet restore "./SMSender.Shared/SMSender.Shared.csproj"
COPY ./SMSender.Processor/ ./SMSender.Processor/
COPY ./SMSender.Shared/ ./SMSender.Shared/

# publish
FROM build AS publish
RUN dotnet publish "./SMSender.Processor/SMSender.Processor.csproj" -c Release -o /app/publish

# prod
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ./SMSender.Processor/wait-for.sh .
RUN ["chmod", "+x", "./wait-for.sh"]
ENTRYPOINT ["dotnet", "SMSender.Processor.dll"]
