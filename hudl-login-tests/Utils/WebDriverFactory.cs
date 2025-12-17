using hudl_login_tests.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using System.Drawing;

public static class WebDriverFactory
{
    private static readonly Dictionary<string, Size> ScreenSizes = new()
    {
        { "desktop", new Size(1920, 1080) },
        { "laptop", new Size(1366, 768) },
        { "tablet", new Size(768, 1024) },
        { "mobile", new Size(375, 812) }
    };

    public static IWebDriver Create()
    {
        var browserType = GetBrowserType().ToLowerInvariant();
        var headless = IsHeadless();
        var screenSize = GetScreenSize();

        var driver = browserType switch
        {
            "firefox" => CreateFirefoxDriver(headless, screenSize),
            "edge" => CreateEdgeDriver(headless, screenSize),
            _ => CreateChromeDriver(headless, screenSize)
        };

        if (!headless)
        {
            driver.Manage().Window.Size = screenSize;
        }

        return driver;
    }

    private static IWebDriver CreateChromeDriver(bool headless, Size screenSize)
    {
        var options = new ChromeOptions();

        if (headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument($"--window-size={screenSize.Width},{screenSize.Height}");
            options.AddArgument("--disable-gpu");
        }

        return new ChromeDriver(options);
    }

    private static IWebDriver CreateFirefoxDriver(bool headless, Size screenSize)
    {
        var options = new FirefoxOptions();

        if (headless)
        {
            options.AddArgument("--headless");
            options.AddArgument($"--width={screenSize.Width}");
            options.AddArgument($"--height={screenSize.Height}");
        }

        return new FirefoxDriver(options);
    }

    private static IWebDriver CreateEdgeDriver(bool headless, Size screenSize)
    {
        var options = new EdgeOptions();

        if (headless)
        {
            options.AddArgument("--headless=new");
            options.AddArgument($"--window-size={screenSize.Width},{screenSize.Height}");
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

    private static Size GetScreenSize()
    {
        var envValue = Environment.GetEnvironmentVariable("BROWSER_SCREENSIZE");
        var sizeKey = !string.IsNullOrEmpty(envValue) ? envValue : TestConfiguration.ScreenSize;

        return ScreenSizes.GetValueOrDefault(sizeKey.ToLowerInvariant(), ScreenSizes["desktop"]);
    }
}
