#!/usr/bin/env bash
set -e
if [ -n "${DOTNETCORE}" ]; then dotnet restore && dotnet build ./Facebook.sln; fi

cd src/Facebook.WebApplication
npm install
npm install -g angular-cli
npm run build-production

