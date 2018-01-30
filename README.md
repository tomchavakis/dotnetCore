# dotnetCore

## Application - Instructions

The Application 

dotnetcore has 2 http endpoints that accepts JSON base64 encoded binary data on both endpoints. After a comparison between these encoded message the result provided to a 3rd endpoint. 

This Application is a dotnet core 2.0 application demostration of web-api technologies. In more details the main application is a dotnet core webapi project. The API use 3rd party library Swagger and makes use of .net standard 2.0 libraries and includes unit tests and integration test created with NUnit. Moreover includes Dockerfile and docker-compose for scale up.

This project developed with VSCode and Rider (JetBrains) 

## Installation Of .NET Core
https://github.com/dotnet/core/blob/master/release-notes/download-archives/2.0.3.md

## Build

dotnet clean
dotnet build

## Run
Move to the WES.WebApp Folder:
dotnet run

and go to the next location:
http://localhost:5000/
and 
http://localhost:5000/swagger/

## Run UnitTests

dotnet test WAES.Tests.UnitTests

## Run IntegrationTests

dotnet test WAES.Test.IntegrationTests --filter DifferencesLeft_AreEqualWithDifferentPayload_ReturnComparisonResult
dotnet test WAES.Test.IntegrationTests --filter DifferencesLeft_AreNotEqual_ReturnComparisonResult

## Run with Docker Compose
Go to WAES.WebApp application folder. 

docker-compose build

docker-compose up

##Integration Tests

You can run the WAES.Test.IntegrationTests. 

Uncomment the //const string basePath = "http://localhost:9001/api"; 