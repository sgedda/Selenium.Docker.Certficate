# Introduction 
This is a .NET CORE 5.0 App that uses Selenium and Docker to make it possible to automatically retrieve a code from a server that must have a secure certificate installed on the client.

# Local https certficates in .NET Core
* dotnet dev-certs https --clean
* dotnet dev-certs https -ep c:\certs\aspnetapp.pfx -p test
* dotnet dev-certs https --trust
 
(https://docs.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-5.0)

# Getting Started
* docker build -t test .
* docker run --rm -it -p 3001:80 -p 3000:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_Kestrel__Certificates__Default__Password="test" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v c:\certs\https:/https/ test



