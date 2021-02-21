#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base

# xvfb for virtual display, for headful ability of selenium in chrome which is needed to get the certificate to work.
RUN apt-get update -y \
  && apt-get -y install \
    xvfb \
  && rm -rf /var/lib/apt/lists/* /var/cache/apt/*

 # Install Chrome
 RUN apt-get update && apt-get install -y \
 apt-transport-https \
 ca-certificates \
 curl \
 gnupg \
 hicolor-icon-theme \
 libcanberra-gtk* \
 libgl1-mesa-dri \
 libgl1-mesa-glx \
 libpango1.0-0 \
 libpulse0 \
 libv4l-0 \
 fonts-symbola \
 libnss3-tools \
 --no-install-recommends \
 && curl -sSL https://dl.google.com/linux/linux_signing_key.pub | apt-key add - \
 && echo "deb [arch=amd64] https://dl.google.com/linux/chrome/deb/ stable main" > /etc/apt/sources.list.d/google.list \
 && apt-get update && apt-get install -y \
 google-chrome-stable \
 --no-install-recommends \
 && apt-get purge --auto-remove -y curl \
 && rm -rf /var/lib/apt/lists/*
 # End Chrome

 
# Set display port and dbus env to avoid hanging
ENV DISPLAY=:99
ENV DBUS_SESSION_BUS_ADDRESS=/dev/null

COPY sshd_config /etc/ssh/
COPY init.sh /usr/local/bin/

WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 9000

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Selenium.Docker.Certificate.csproj", ""]
RUN dotnet restore "./Selenium.Docker.Certificate.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Selenium.Docker.Certificate.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Selenium.Docker.Certificate.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY auto_select_certificate.json $HOME/etc/opt/chrome/policies/managed/auto_select_certificate.json
COPY auto_select_certificate.json $HOME/etc/opt/auto_select_certificate.json
ENTRYPOINT ["bash","init.sh"]