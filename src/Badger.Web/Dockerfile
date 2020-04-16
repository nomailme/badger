FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build

ARG BUILDCONFIG=RELEASE
ARG VERSION=1.0.0

COPY . ./build/

#RUN dotnet restore

WORKDIR /build/
RUN dotnet publish Badger.Web.csproj -r alpine-x64 --self-contained true  -c $BUILDCONFIG -o /build/out /p:Version=$VERSION /p:PublishTrimmed=true

FROM alpine:3.9.4
EXPOSE 80
WORKDIR /app

# Add some libs required by .NET runtime 
RUN apk add --no-cache libstdc++ libintl

COPY --from=build /build/out .

ENTRYPOINT ["./Badger.Web"]