dotnet test --collect:"XPlat Code Coverage"
$latestTestFolder = Get-ChildItem .\tests\JosiArchitecture.Tests\TestResults\ | ? { $_.PSIsContainer } | sort CreationTime -desc | select -f 1
reportgenerator -reports:"tests\JosiArchitecture.Tests\TestResults\${latestTestFolder}\coverage.cobertura.xml" -targetdir:"CoverageReport" -reporttypes:Html
serve CoverageReport
rmdir tests\JosiArchitecture.Tests\TestResults\ -Force -Recurse
rmdir CoverageReport -Force -Recurse