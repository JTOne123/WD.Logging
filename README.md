# WebDucer library for logging in Xamarin.Forms (Prism)

A generic abstraction for the logger to be used with dependency injection in Xamarin.Forms (e.g. with Prism.Forms).

## States

| Service | Last | Develop | Master |
| :------ | ---: | ------: | -----: |
| AppVeyor | [![Build status last](https://ci.appveyor.com/api/projects/status/mrn4h99t5auxc265?svg=true)](https://ci.appveyor.com/project/WebDucer/wd-logging) | [![Build status develop](https://ci.appveyor.com/api/projects/status/mrn4h99t5auxc265/branch/develop?svg=true)](https://ci.appveyor.com/project/WebDucer/wd-logging/branch/develop) | [![Build status master](https://ci.appveyor.com/api/projects/status/mrn4h99t5auxc265/branch/master?svg=true)](https://ci.appveyor.com/project/WebDucer/wd-logging/branch/master) |
| SonarCube coverage | | [![SonarQube Coverage](https://sonarcloud.io/api/project_badges/measure?branch=develop&project=WD.Logging&metric=coverage)](https://sonarcloud.io/dashboard?branch=develop&id=WD.Logging) | [![SonarQube Coverage](https://sonarcloud.io/api/project_badges/measure?project=WD.Logging&metric=coverage)](https://sonarcloud.io/dashboard?id=WD.Logging) 
| SonarCube technical debt | | [![SonarQube Technical Debt](https://sonarcloud.io/api/project_badges/measure?branch=develop&project=WD.Logging&metric=sqale_index)](https://sonarcloud.io/dashboard?branch=develop&id=WD.Logging) | [![SonarQube Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=WD.Logging&metric=sqale_index)](https://sonarcloud.io/dashboard?id=WD.Logging) |
| SonarCube Quality Gate | | [![SonarQube Quality Gate](https://sonarcloud.io/api/project_badges/measure?branch=develop&project=WD.Logging&metric=alert_status)](https://sonarcloud.io/dashboard?branch=develop&id=WD.Logging) | [![SonarQube Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=WD.Logging&metric=alert_status)](https://sonarcloud.io/dashboard?id=WD.Logging) |
| NuGet | [![NuGet](https://img.shields.io/nuget/dt/WD.Logging.svg)](https://www.nuget.org/packages/WD.Logging) | [![NuGet Pre Release](https://img.shields.io/nuget/vpre/WD.Logging.svg)](https://www.nuget.org/packages/WD.Logging) | [![NuGet](https://img.shields.io/nuget/v/WD.Logging.svg)](https://www.nuget.org/packages/WD.Logging) |

## Services

- `ILogger<>` / `ILogger` - Abstraction for logger
- `ILoggerConfiguration` - Abstraction, to configure the current logger with code

## Sample

### Registration

```csharp
...
containerRegistry.RegisterSingleton<ILoggerConfiguration, NLogLoggerConfiguration>();
containerRegistry.RegisterSingleton(typeof(ILogger<>), typeof(NLogLoggerAdapter<>));
...
```

### Configuration

```csharp
private void InitLogger()
{
    var logConfiguration = Container.Resolve<ILoggerConfiguration>();
    logConfiguration.ApplyConfiguration(options =>
        options
            .WithFile("RaumAkustik.log")
            .WithLevel(LogLevel.Trace)
            .WithMaxSize(new FileSize { SizeType = SizeType.MibiByte, Size = 2 })
            .WithArchiveCount(6)
            .Compress(false)
            .ArchiveOnStart(false)
            .WithLogMessageLayout("${longdate};${logger};${message}")
            .WithFilter("MyAssembly.Namespace.MyClass*")
#if DEBUG
            .AddDebugTarget()
#endif
    );
}
```

### Change configuration

```csharp
private void SetLogLevel(LogLevel level, string newFilter)
{
    _loggerConfiguration.ChangeConfiguration(options =>
        options.WithLevel(level)
            .WithFilter(newFilter)
    );
}
```

### Usage

```csharp
...
public class MyClass
{
    private readonly ILogger _looger;

    public MyClass(ILogger<MyClass> logger)
    {
        _logger = logger;
    }
...
    private async Task SomeMethod()
    {
        try {
            _logger.Trace("Start processing");

            var data = await ProcessSomeData();

            _logger.Trace("Finsih processing");
        } catch (Exception ex){
            _logger.Error(ex, "Processing failed for {0}", nameof(ProcessSomeData));
        }
    }
...
```