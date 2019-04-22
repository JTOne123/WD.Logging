public static class Names {
    public const string DEFAULT_CONFIGURATION = "Release";
    public const string SONARCUBE_API_TOKEN = "CI_CONAR_TOKEN";
    public const string SONARQUBE_URI = "CI_SONAR_URL";
    public const string NUGET_URI = "NUGET_REPO_URL";
    public const string NUGET_API_TOKEN = "NUGET_API_KEY";

    public const string PROJECT_ID = "WD.Logging";
    public const string PROJECT_ID_ABSTRACTIONS = "WD.Logging.Abstractions";

    public const string PROJECT_TITLE = "WebDucer logging library";
    public const string PROJECT_TITLE_ABSTRACTIONS = "WebDucer abstractions for logging library";
    public static readonly string[] PROJECT_AUTHORS = {"Eugen [WebDucer] Richter"};
    public static readonly string[] PROJECT_OWNERS = {"Eugen [WebDucer] Richter"};
    public const string PROJECT_DESCRIPTION = @"WebDucer library for handle logging in Xamarin.Forms with dependency injection";
    public const string PROJECT_DESCRIPTION_ABSTRACTIONS = @"WebDucer abstractions for logger and logconfiguration";
    public static readonly string PROJECT_COPYRIGHTS = string.Format("MIT - (c) {0} Eugen [WebDucer] Richter", DateTime.Now.Year);
    public static readonly string[] PROJECT_TAGS = {"Xamarin", "Xamarin.Forms", "WebDucer", "Forms", "Logging", "Log", "NLog"};

    public const string SONARQUBE_ORGANISATION = "webducer-oss";
}