FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build

ARG BUILDCONFIG=RELEASE
ARG VERSION=1.0.0

COPY . ./build/

#RUN dotnet restore

WORKDIR /build/
RUN dotnet publish Badger.Web.csproj -r linux-x64 --self-contained true  -c $BUILDCONFIG -o /build/out /p:PublishTrimmed=true /p:Version=$VERSION

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
EXPOSE 80
WORKDIR /app

# Add some libs required by .NET runtime 
#RUN apk add --no-cache libstdc++ libintl wget && \
#wget -q -O /etc/apk/keys/sgerrand.rsa.pub https://alpine-pkgs.sgerrand.com/sgerrand.rsa.pub && \
#wget https://github.com/sgerrand/alpine-pkg-glibc/releases/download/2.28-r0/glibc-2.28-r0.apk && \
#apk add glibc-2.28-r0.apk
RUN apt-get update \
 && apt-get install -y cabextract wget xfonts-utils \
 && curl -s -o ttf-mscorefonts-installer_3.7_all.deb http://ftp.us.debian.org/debian/pool/contrib/m/msttcorefonts/ttf-mscorefonts-installer_3.7_all.deb \
 && sh -c "echo ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true | debconf-set-selections" \
 && dpkg -i ttf-mscorefonts-installer_3.7_all.deb

COPY --from=build /build/out .

ENTRYPOINT ["./Badger.Web"]