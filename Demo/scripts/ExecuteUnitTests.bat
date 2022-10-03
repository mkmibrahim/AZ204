rmdir /q/s ..\backend\UnitTestCoverageOutput\

set location=%cd%

Dotnet test ..\Demo.sln /p:configuration=Release /maxcpucount:1 /p:CollectCoverage=true /p:CoverletOutputFormat=\"cobertura,opencover,json\"  /p:CoverletOutput="..\UnitTestCoverageOutput\\" --logger:trx;LogFileName=unit_tests.xml /p:MergeWith="..\UnitTestCoverageOutput\coverage.json" --filter Category!=Integration

cd %location%

dotnet  %UserProfile%\.nuget\packages\reportgenerator\4.8.11\tools\net5.0\ReportGenerator.dll "-reports:..\backend\UnitTestCoverageOutput\coverage.cobertura.xml" "-targetdir:..\backend\UnitTestCoverageOutput\html" -reporttypes:Html
	start ..\backend\UnitTestCoverageOutput\html\index.html