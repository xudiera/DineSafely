# Build stage image
FROM --platform=$BUILDPLATFORM  mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
ENV HUSKY=0
ARG configuration=Release
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY src/Web/Web.csproj .
RUN dotnet restore

# Copy everything else and publish app
COPY src/Web/. .
RUN dotnet publish -c $configuration -o /app --no-restore

# Final stage image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-jammy-chiseled
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["./Web"]