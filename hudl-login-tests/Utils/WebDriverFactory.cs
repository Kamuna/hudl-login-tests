using hudl_login_tests.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

public static class WebDriverFactory
{
    public static IWebDriver Create()
    {
        var browserType = GetBrowserType().ToLowerInvariant();
        var headless = IsHeadless();

        return browserType switch
        {
            "firefox" => CreateFirefoxDriver(headless),
            "edge" => CreateEdgeDriver(headless),
            _ => CreateChromeDriver(headless)
        };
    }

    private static IWebDriver CreateChromeDriver(bool headless)
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");

        if (headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--disable-gpu");
        }

        return new ChromeDriver(options);
    }

    private static IWebDriver CreateFirefoxDriver(bool headless)
    {
        var options = new FirefoxOptions();

        if (headless)
        {
            options.AddArgument("--headless");
            options.AddArgument("--width=1920");
            options.AddArgument("--height=1080");
        }

        return new FirefoxDriver(options);
    }

    private static IWebDriver CreateEdgeDriver(bool headless)
    {
        var options = new EdgeOptions();
        options.AddArgument("--start-maximized");

        if (headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--disable-gpu");
        }

        return new EdgeDriver(options);
    }

    private static string GetBrowserType()
    {
        var envValue = Environment.GetEnvironmentVariable("BROWSER_TYPE");
        if (!string.IsNullOrEmpty(envValue))
        {
            return envValue;
        }

        return TestConfiguration.BrowserType;
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
