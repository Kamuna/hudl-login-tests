using hudl_login_tests.Configuration;

namespace hudl_login_tests.Utils
{
    public static class TestDataProvider
    {
        public static IEnumerable<object[]> InvalidEmailTestData
        {
            get
            {
                yield return new object[] { "", TestConfiguration.ExpectedEmptyEmailMessage };
                yield return new object[] { "invalid-email-format", TestConfiguration.ExpectedInvalidEmailMessage };
                yield return new object[] { "notarealemail", TestConfiguration.ExpectedInvalidEmailMessage };
            }
        }

        public static IEnumerable<object[]> BoundaryTestData
        {
            get
            {
                var length = TestConfiguration.BoundaryTestLength;
                yield return new object[] { "email", length };
                yield return new object[] { "password", length };
            }
        }

        public static IEnumerable<object[]> EmailCaseSensitivityTestData
        {
            get
            {
                var email = TestConfiguration.ValidUserEmail;

                yield return new object[] { email.ToUpperInvariant() };

                // Mixed case - capitalize first letter of each part
                yield return new object[] { ToTitleCase(email) };

                // Alternating case
                yield return new object[] { ToAlternatingCase(email) };
            }
        }

        private static string ToTitleCase(string email)
        {
            var parts = email.Split('@');
            if (parts.Length != 2) return email;

            var localPart = string.Join(".", parts[0].Split('.').Select(CapitalizeFirstLetter));
            var domainPart = string.Join(".", parts[1].Split('.').Select(CapitalizeFirstLetter));

            return $"{localPart}@{domainPart}";
        }

        private static string CapitalizeFirstLetter(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            return char.ToUpperInvariant(s[0]) + s.Substring(1).ToLowerInvariant();
        }

        private static string ToAlternatingCase(string email)
        {
            var chars = email.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = i % 2 == 0
                    ? char.ToLowerInvariant(chars[i])
                    : char.ToUpperInvariant(chars[i]);
            }
            return new string(chars);
        }
    }
}
