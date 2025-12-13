using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace hudl_login_tests.Configuration
{
    public static class TestConfiguration
    {
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
    }
}
