# build the application using CBL-Mariner 2.0
FROM mcr.microsoft.com/dotnet/sdk:6.0-cbl-mariner2.0 AS build

# copy in the project file and restore dependencies
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore --runtime linux-x64

# copy in all source files
COPY . ./

# publish a trimmed application
RUN dotnet publish --no-restore -c Release -o out --runtime linux-x64 --self-contained true -p:PublishTrimmed=true -p:PublishSingleFile=true -p:LinkMode=trim

# run the application on a 'distroless' image
FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-cbl-mariner2.0-distroless AS runtime

# copy in the built application
WORKDIR /app
COPY --from=build /app/out ./

# use a non-privileged port; https://github.com/dotnet/dotnet-docker/issues/3796
ENV ASPNETCORE_URLS=http://+:8080

# change the entrypoint name to match your web application
ENTRYPOINT ["./webapp"]
