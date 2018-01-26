# Use microsoft/aspnetcore-build:2.0 from Linux
# Use microsoft/aspnetcore:2.0 from Microsoft
FROM microsoft/aspnetcore-build:2.0
ARG source
WORKDIR /app
EXPOSE 80
COPY /bin/Release/netcoreapp2.0/publish .
RUN ls -la
ENTRYPOINT ["dotnet", "WebbApp.dll"]