using OpenQA.Selenium;

namespace hudl_login_tests.Utils
{
    public static class ScreenshotHelper
    {
        public static void TakeScreenshot(IWebDriver driver, string testName)
        {
            try
            {
                var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
                var screenshotDir = Path.Combine(projectRoot, "TestResults", "Screenshots");
                Directory.CreateDirectory(screenshotDir);

                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();

                string sanitizedTestName = SanitizeFileName(testName);
                string fileName = $"{sanitizedTestName}_{DateTime.Now:yyyyMMddHHmmss}.png";
                string filePath = Path.Combine(screenshotDir, fileName);

                screenshot.SaveAsFile(filePath);
                Console.WriteLine($"Screenshot saved to: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to take screenshot: {ex.Message}");
            }
        }

        private static string SanitizeFileName(string name)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            return string.Join("_", name.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
        }
    }
}
