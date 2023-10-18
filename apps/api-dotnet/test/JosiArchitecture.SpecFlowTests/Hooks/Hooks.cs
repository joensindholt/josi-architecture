using System.Diagnostics.CodeAnalysis;
using JosiArchitecture.IntegrationTests.TestEnvironments;

namespace JosiArchitecture.SpecFlowTests.Hooks;

[Binding]
[SuppressMessage("Major Code Smell", "S1118:Utility classes should not have public constructors")]
public class Hooks
{
    private static ITestEnvironment? _testEnvironment;

    public static HttpClient Client { get; private set; } = null!;

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        _testEnvironment = new ExternallyHostedEnvironment();
        _testEnvironment.InitializeAsync().Wait();
        Client = _testEnvironment.Client;
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        _testEnvironment?.DisposeAsync().Wait();
    }
}
