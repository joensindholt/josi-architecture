using JosiArchitecture.IntegrationTests;

namespace JosiArchitecture.SpecFlowTests.Hooks
{
    [Binding]
    public class Hooks
    {
        private static TestEnvironment? _testEnvironment;

        public static HttpClient? Client { get; private set; }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _testEnvironment = new TestEnvironment();
            _testEnvironment.InitializeAsync().Wait();
            Client = _testEnvironment.Client;
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _testEnvironment?.DisposeAsync().AsTask().Wait();
        }
    }
}