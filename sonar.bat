dotnet sonarscanner begin /k:"HackerNews" /d:sonar.host.url="http://localhost:9000"  /d:sonar.login="583ee2e88bc5012367ba1e49f3a2d629658c22ce"
dotnet build
dotnet sonarscanner end /d:sonar.login="583ee2e88bc5012367ba1e49f3a2d629658c22ce"