using hudl_login_tests.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public static class WebDriverFactory
{
    public static IWebDriver Create()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");

        if (IsHeadless())
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--disable-gpu");
        }

        return new ChromeDriver(options);
    }

    private static bool IsHeadless()
    {
        var envValue = Environment.GetEnvironmentVariable("BROWSER_HEADLESS");
        if (!string.IsNullOrEmpty(envValue))
        {
            return bool.TryParse(envValue, out var envResult) && envResult;
        }

        return TestConfiguration.Headless;
    }
}
