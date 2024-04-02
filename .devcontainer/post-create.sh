#!/bin/sh

# HTTPS certificate
dotnet dev-certs https
sudo -E dotnet dev-certs https -ep /usr/local/share/ca-certificates/aspnet/https.crt --format PEM
sudo update-ca-certificates

# Restore dotnet tools
dotnet tool restore

# Apply migrations to the database
cd src/Web/
dotnet ef database update
