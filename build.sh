#!/usr/bin/env bash
set -e

if [[ "$TRAVIS_OS_NAME" == "osx" ]]; then brew update; fi
if [[ "$TRAVIS_OS_NAME" == "osx" ]]; then rvm --default use ruby-2.3.3; fi
if [ -n "${DOTNETCORE}" ]; then dotnet restore && dotnet build ./Facebook.sln; fi

cd src/Facebook.WebApplication
npm install
npm install -g angular-cli
npm run build-production

