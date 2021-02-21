# Introduction 
This is a .NET CORE 5.0 App that uses Selenium and Docker to make it possible to automatically retrieve a code from a server that must have a secure certificate installed on the client.

# Getting Started
* docker build -t test .
* docker run --rm -it -p 3001:80 -p 3000:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_Kestrel__Certificates__Default__Password="test" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v %USERPROFILE%\.aspnet\https:/https/ test
(https://docs.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-5.0)
