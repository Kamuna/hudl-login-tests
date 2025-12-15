using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace hudl_login_tests.Configuration
{
    public static class TestConfiguration
    {
        public static bool Headless =>
            bool.TryParse(ConfigurationProvider.Get("Browser:Headless"), out var result) && result;

        public static string HomepageUrl =>
            ConfigurationProvider.Get("Hudl:HomepageUrl");

        public static string ValidUserEmail =>
            ConfigurationProvider.Get("Hudl:ValidUser:Email");

        public static string ValidUserPassword =>
            ConfigurationProvider.Get("Hudl:ValidUser:Password");

        public static string InvalidPasswordUserEmail =>
            ConfigurationProvider.Get("Hudl:InvalidUsers:0:Email");

        public static string InvalidPasswordUserPassword =>
            ConfigurationProvider.Get("Hudl:InvalidUsers:0:Password");

        public static string InvalidEmailUserEmail =>
            ConfigurationProvider.Get("Hudl:InvalidUsers:1:Email");

        public static string InvalidEmailUserPassword =>
            ConfigurationProvider.Get("Hudl:InvalidUsers:1:Password");

        public static string WrongEmail =>
            ConfigurationProvider.Get("TestData:WrongEmail");

        public static string SamplePassword =>
            ConfigurationProvider.Get("TestData:SamplePassword");

        public static int BoundaryTestLength =>
            ConfigurationProvider.Get<int>("TestData:BoundaryTestLength");

        public static string EmailDomainSuffix =>
            ConfigurationProvider.Get("TestData:EmailDomainSuffix");

        public static string ExpectedEmptyEmailMessage =>
            ConfigurationProvider.Get("ExpectedMessages:EmptyEmail");

        public static string ExpectedInvalidEmailMessage =>
            ConfigurationProvider.Get("ExpectedMessages:InvalidEmail");

        public static string ExpectedEmptyPasswordMessage =>
            ConfigurationProvider.Get("ExpectedMessages:EmptyPassword");

        public static string ExpectedInvalidCredentialsMessage =>
            ConfigurationProvider.Get("ExpectedMessages:InvalidCredentials");

        public static string LoginUrlPattern =>
            ConfigurationProvider.Get("Urls:LoginUrlPattern");

        public static string LoggedOutUrl =>
            ConfigurationProvider.Get("Urls:LoggedOutUrl");

        public static string HomePageUrl =>
            ConfigurationProvider.Get("Urls:HomePageUrl");
    }
}
