using System.Text.Json;

namespace hudl_login_tests.Utils
{
    public static class TestDataLoader
    {
        private static readonly Lazy<TestDataRoot> _testData = new(() => LoadTestData());

        public static TestDataRoot Data => _testData.Value;

        private static TestDataRoot LoadTestData()
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testData.json");
            
            if (!File.Exists(filePath))
            {
                filePath = "testData.json";
            }

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<TestDataRoot>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? throw new InvalidOperationException("Failed to load test data from testData.json");
        }
    }

    public class TestDataRoot
    {
        public List<InvalidEmailTestCase> InvalidEmailTestCases { get; set; } = new();
        public List<BoundaryTestCase> BoundaryTestCases { get; set; } = new();
        public List<LoginTestCase> LoginTestCases { get; set; } = new();
        public ExpectedMessagesConfig ExpectedMessages { get; set; } = new();
        public TestValuesConfig TestValues { get; set; } = new();
        public UrlsConfig Urls { get; set; } = new();
    }

    public class InvalidEmailTestCase
    {
        public string Email { get; set; } = string.Empty;
        public string ExpectedMessage { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class BoundaryTestCase
    {
        public string FieldName { get; set; } = string.Empty;
        public int Length { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class LoginTestCase
    {
        public string Scenario { get; set; } = string.Empty;
        public bool UseValidCredentials { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ExpectedResult { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class ExpectedMessagesConfig
    {
        public string EmptyEmail { get; set; } = string.Empty;
        public string InvalidEmail { get; set; } = string.Empty;
        public string EmptyPassword { get; set; } = string.Empty;
        public string InvalidCredentials { get; set; } = string.Empty;
    }

    public class TestValuesConfig
    {
        public string WrongEmail { get; set; } = string.Empty;
        public string SamplePassword { get; set; } = string.Empty;
        public string EmailDomainSuffix { get; set; } = string.Empty;
    }

    public class UrlsConfig
    {
        public string LoginUrlPattern { get; set; } = string.Empty;
        public string LoggedOutUrl { get; set; } = string.Empty;
        public string HomePageUrl { get; set; } = string.Empty;
    }
}
