FROM microsoft/dotnet:sdk AS build
WORKDIR /code
COPY *.csproj /code
RUN dotnet restore

COPY . /code
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:runtime
WORKDIR /app
COPY --from=build /code/out /app
ENTRYPOINT ["dotnet","console.dll"]