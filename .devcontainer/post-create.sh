#!/bin/sh

# Restore dotnet tools
dotnet tool restore

# Apply migrations to the database
cd src/Web/
dotnet ef database update
