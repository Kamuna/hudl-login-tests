using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using hudl_login_tests.Utils;

namespace hudl_login_tests.Configuration
{
    public static class TestConfiguration
    {
        public static bool Headless =>
            bool.TryParse(ConfigurationProvider.Get("Browser:Headless"), out var result) && result;

        public static string BrowserType =>
            ConfigurationProvider.Get("Browser:Type");

        public static string ScreenSize =>
            ConfigurationProvider.GetOrDefault("Browser:ScreenSize", "desktop");

        public static string HomepageUrl =>
            ConfigurationProvider.Get("Hudl:HomepageUrl");

        public static string ValidUserEmail =>
            ConfigurationProvider.Get("Hudl:ValidUser:Email");

        public static string ValidUserPassword =>
            ConfigurationProvider.Get("Hudl:ValidUser:Password");

        public static string InvalidPasswordUserEmail =>
            TestDataLoader.Data.LoginTestCases.First(x => x.Scenario == "InvalidPassword").Email;

        public static string InvalidPasswordUserPassword =>
            TestDataLoader.Data.LoginTestCases.First(x => x.Scenario == "InvalidPassword").Password;

        public static string InvalidEmailUserEmail =>
            TestDataLoader.Data.LoginTestCases.First(x => x.Scenario == "InvalidEmail").Email;

        public static string WrongEmail =>
            TestDataLoader.Data.TestValues.WrongEmail;

        public static string SamplePassword =>
            TestDataLoader.Data.TestValues.SamplePassword;

        public static string EmailDomainSuffix =>
            TestDataLoader.Data.TestValues.EmailDomainSuffix;

        public static string ExpectedEmptyEmailMessage =>
            TestDataLoader.Data.ExpectedMessages.EmptyEmail;

        public static string ExpectedInvalidEmailMessage =>
            TestDataLoader.Data.ExpectedMessages.InvalidEmail;

        public static string ExpectedEmptyPasswordMessage =>
            TestDataLoader.Data.ExpectedMessages.EmptyPassword;

        public static string ExpectedInvalidCredentialsMessage =>
            TestDataLoader.Data.ExpectedMessages.InvalidCredentials;

        public static string PasswordResetSuccessMessage =>
            TestDataLoader.Data.ExpectedMessages.PasswordResetSuccess;

        public static string LoginUrlPattern =>
            TestDataLoader.Data.Urls.LoginUrlPattern;

        public static string LoggedOutUrl =>
            TestDataLoader.Data.Urls.LoggedOutUrl;

        public static string HomePageUrl =>
            TestDataLoader.Data.Urls.HomePageUrl;
    }
}
