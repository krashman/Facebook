#!/usr/bin/env bash

if [ -n "${DOTNETCORE}" ]; then dotnet restore && dotnet build ./Facebook.sln; fi

cd src/Facebook.WebApplication
npm run build-production
