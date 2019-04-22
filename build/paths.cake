using System.Xml.Linq;

public static class Paths {
    private const string _BASE_PATH = "./BuildOutput/";
    public const string BUILD_OUTPUT = _BASE_PATH + "Build/";
    public const string TEST_OUTPUT = _BASE_PATH + "Tests/";
    public const string ARTIFACTS_OUTPUT = _BASE_PATH + "Artifacts/";
    public const string SONARQUBE_OUTPUT = "./.sonarqube/";

    public const string SOLUTION_FILE = "./WD.Logging.sln";
    public const string SOLUTION_FILE_FOR_SONAR = "./WD.Logging.Sonar.sln";
    public const string PROJECT_FILE = "./src/Logging/Logging.csproj";
    public const string ASSEMBLY_INFO_FILE = "./src/GlobalAssemblyInfo.cs";

    public const string TEST_PROJECT_FILE = "./tests/Logging.Tests/Logging.Tests.csproj";
    public const string TEST_RESULT_FILE = "TestResults.trx";
    public const string TEST_COVERAGE_RESULT_FILE = ARTIFACTS_OUTPUT + "TestCoverage.dcvr";
    public const string TEST_COVERAGE_RESULT_FILE_XML = ARTIFACTS_OUTPUT + "TestCoverage.xml";
    public const string TEST_COVERAGE_RESULT_FILE_HTML = ARTIFACTS_OUTPUT + "TestCoverage.html";
    public const string RELEASE_NOTES_FILE = "./CHANGELOG";

    public static readonly Uri LICENSE_URL = new Uri("https://github.com/WebDucer/WD.Logging/blob/develop/LICENSE");
    public static readonly Uri PROJECT_URL = new Uri("https://github.com/WebDucer/WD.Logging");
    public const string SOURCE_URL = "https://github.com/WebDucer/WD.Logging.git";

    public static IList<NuSpecDependency> GetDependenciesFromProjectFile(string projectFile, string targetFramework = null){
        return XDocument.Load(projectFile)
            .Descendants("PackageReference")
            .Select(s => new NuSpecDependency {
                Id = (string)s.Attribute("Include"),
                Version = (string)s.Attribute("Version") ?? (string)s.Element("Version"),
                TargetFramework = targetFramework })
            .Where(w => !w.Id.StartsWith("Microsoft.SourceLink"))
            .ToList();
    }

}