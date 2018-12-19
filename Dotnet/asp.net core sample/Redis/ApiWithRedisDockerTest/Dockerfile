FROM microsoft/dotnet:2.2-sdk as build-env
WORKDIR /code
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Rlease -o output

From microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /code/output ./

EXPOSE 5000
ENTRYPOINT [ "dotnet","ApiWithRedisDockerTest.dll" ]