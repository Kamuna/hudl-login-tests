using hudl_login_tests.Pages;
using hudl_login_tests.Utils;
using OpenQA.Selenium;

namespace hudl_login_tests.Features.StepDefinitions
{
    /// <summary>
    /// Base class for all step definitions providing common functionality.
    /// </summary>
    public abstract class BaseSteps : IDisposable
    {
        protected readonly IWebDriver Driver;
        protected readonly HudlHomePage HomePage;
        protected readonly HudlIdentityLoginPage LoginPage;
        protected readonly HudlDashboardPage Dashboard;

        private Exception? _lastException;
        private string _currentStepName = "Unknown";

        protected BaseSteps(IWebDriver driver)
        {
            Driver = driver;
            HomePage = new HudlHomePage(driver);
            LoginPage = new HudlIdentityLoginPage(driver);
            Dashboard = new HudlDashboardPage(driver);
        }

        /// <summary>
        /// Executes a step with error handling and screenshot capture on failure.
        /// </summary>
        protected async Task ExecuteStep(string stepName, Action action)
        {
            _currentStepName = stepName;
            await Task.Run(() =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    _lastException = ex;
                    throw;
                }
            });
        }

        #region Assertion Helpers

        /// <summary>
        /// Asserts that a condition is true with a custom message.
        /// </summary>
        protected void AssertTrue(bool condition, string message)
        {
            Assert.True(condition, message);
        }

        /// <summary>
        /// Asserts that actual string contains expected substring.
        /// </summary>
        protected void AssertContains(string expectedSubstring, string actual)
        {
            Assert.Contains(expectedSubstring, actual);
        }

        /// <summary>
        /// Asserts that two values are equal.
        /// </summary>
        protected void AssertEqual<T>(T expected, T actual, string message = "")
        {
            if (string.IsNullOrEmpty(message))
                Assert.Equal(expected, actual);
            else
                Assert.True(Equals(expected, actual), message);
        }

        #endregion

        public virtual void Dispose()
        {
            if (_lastException != null)
            {
                ScreenshotHelper.TakeScreenshot(Driver, $"Failed_{_currentStepName}");
            }

            Driver?.Quit();
            Driver?.Dispose();
        }
    }
}
