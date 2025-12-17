using Microsoft.Extensions.Configuration;

namespace hudl_login_tests.Configuration
{
    public static class ConfigurationProvider
    {
        private static IConfigurationRoot? _configurationRoot;

        private static IConfigurationRoot ConfigurationRoot => _configurationRoot ??=
        new ConfigurationBuilder()
            .AddJsonFile("testSettings.json")
            .AddJsonFile("testSettings.local.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static string Get(string key) => ConfigurationRoot[key]
        ?? throw new ArgumentOutOfRangeException(nameof(key), $"Configuration for {key} not found.");

        public static string GetOrDefault(string key, string defaultValue) =>
            ConfigurationRoot[key] ?? defaultValue;

        public static T Get<T>(string key)
        {
            var valueString = Get(key);
            try
            {
                var value = Convert.ChangeType(valueString, typeof(T));
                return (T)value;
            }
            catch
            {
                var message = $"Configuration for '{key}' value '{valueString}' could not be converted to {typeof(T).Name}.";
                throw new InvalidCastException(message);
            }
        }
    }
}
