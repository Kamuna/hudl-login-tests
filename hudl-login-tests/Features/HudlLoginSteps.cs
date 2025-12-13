using hudl_login_tests.Configuration;
using hudl_login_tests.Pages;
using hudl_login_tests.Utils;
using OpenQA.Selenium;


namespace hudl_login_tests.Features
{
    public class HudlLoginSteps : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly HudlHomePage _homePage;
        private readonly HudlIdentityLoginPage _loginPage;
        private readonly HudlDashboardPage _dashboard;
        private Exception? _lastException;
        private string _currentStepName = "Unknown";

        public HudlLoginSteps(IWebDriver driver)
        {
            _driver = driver;
            _homePage = new HudlHomePage(driver);
            _loginPage = new HudlIdentityLoginPage(driver);
            _dashboard = new HudlDashboardPage(driver);
        }

        private async Task ExecuteStep(string stepName, Action action)
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

        public async Task AUserNavigatesToTheLoginPage()
        {
            await ExecuteStep(nameof(AUserNavigatesToTheLoginPage), () =>
            {
                _homePage.Open(TestConfiguration.HomepageUrl);
                _homePage.GoToHudlLogin();
            });
        }

        public async Task UserLoginsInWithValidCredentials()
        {
            await ExecuteStep(nameof(UserLoginsInWithValidCredentials), () =>
            {
                _loginPage.Login(TestConfiguration.ValidUserEmail, TestConfiguration.ValidUserPassword);
            });
        }

        public async Task UserShouldBeLoggedInSuccessfully()
        {
            await ExecuteStep(nameof(UserShouldBeLoggedInSuccessfully), () =>
            {
                Assert.True(_dashboard.IsOnHomePage(), "User should be redirected to https://www.hudl.com/home");

                _dashboard.ClickProfileMenu();
                Assert.True(_dashboard.IsLogOutVisible(), "'Log Out' should be visible");
            });
        }

        #region Invalid Credentials Steps

        public async Task UserLoginsInWithInvalidPassword()
        {
            await ExecuteStep(nameof(UserLoginsInWithInvalidPassword), () =>
            {
                _loginPage.Login(TestConfiguration.InvalidPasswordUserEmail, TestConfiguration.InvalidPasswordUserPassword);
            });
        }

        public async Task UserLoginsInWithInvalidEmail()
        {
            await ExecuteStep(nameof(UserLoginsInWithInvalidEmail), () =>
            {
                _loginPage.EnterEmail(TestConfiguration.InvalidEmailUserEmail);
                _loginPage.ClickContinue();
            });
        }

        public async Task UserEntersEmailAndClicksContinue(string email)
        {
            await ExecuteStep($"UserEntersEmailAndClicksContinue_{email}", () =>
            {
                if (string.IsNullOrEmpty(email))
                {
                    _loginPage.SubmitEmptyEmail();
                }
                else
                {
                    _loginPage.EnterEmail(email);
                    _loginPage.ClickContinue();
                }
            });
        }

        public async Task AnErrorMessageShouldBeDisplayed()
        {
            await ExecuteStep(nameof(AnErrorMessageShouldBeDisplayed), () =>
            {
                Assert.True(_loginPage.IsErrorDisplayed(), "An error message should be displayed for invalid credentials");
            });
        }

        public async Task AnInvalidEmailErrorMessageShouldBeDisplayed()
        {
            await ExecuteStep(nameof(AnInvalidEmailErrorMessageShouldBeDisplayed), () =>
            {
                Assert.True(_loginPage.IsInvalidEmailErrorDisplayed(), "An invalid email error message should be displayed");
                var errorText = _loginPage.GetInvalidEmailErrorText();
                Assert.Contains("Enter a valid email", errorText);
            });
        }

        public async Task AnInvalidPasswordErrorMessageShouldBeDisplayed()
        {
            await ExecuteStep(nameof(AnInvalidPasswordErrorMessageShouldBeDisplayed), () =>
            {
                Assert.True(_loginPage.IsInvalidPasswordErrorDisplayed(), "An invalid password error message should be displayed");
                var errorText = _loginPage.GetInvalidPasswordErrorText();
                Assert.Contains("Incorrect username or password", errorText);
            });
        }

        #endregion

        #region Validation Steps

        public async Task UserSubmitsEmptyEmail()
        {
            await ExecuteStep(nameof(UserSubmitsEmptyEmail), () =>
            {
                _loginPage.SubmitEmptyEmail();
            });
        }

        public async Task UserSubmitsEmptyPassword()
        {
            await ExecuteStep(nameof(UserSubmitsEmptyPassword), () =>
            {
                _loginPage.EnterEmail(TestConfiguration.ValidUserEmail);
                _loginPage.SubmitEmptyPassword();
            });
        }

        public async Task UserEntersInvalidEmailFormat()
        {
            await ExecuteStep(nameof(UserEntersInvalidEmailFormat), () =>
            {
                _loginPage.EnterEmail("invalid-email-format");
                _loginPage.ClickContinue();
            });
        }

        public async Task AValidationErrorShouldBeDisplayed()
        {
            await ExecuteStep(nameof(AValidationErrorShouldBeDisplayed), () =>
            {
                Assert.True(_loginPage.IsValidationErrorDisplayed(), "A validation error should be displayed");
            });
        }

        #endregion

        #region Logout Steps

        public async Task UserClicksLogout()
        {
            await ExecuteStep(nameof(UserClicksLogout), () =>
            {
                // Ensure profile menu is open before clicking logout
                _dashboard.ClickProfileMenu();
                _dashboard.ClickLogout();
            });
        }

        public async Task UserShouldBeLoggedOut()
        {
            await ExecuteStep(nameof(UserShouldBeLoggedOut), () =>
            {
                Assert.True(_homePage.IsOnHomePage(), "User should be redirected to the home page after logout");
            });
        }

        #endregion

        public void Dispose()
        {
            if (_lastException != null)
            {
                ScreenshotHelper.TakeScreenshot(_driver, $"Failed_{_currentStepName}");
            }

            _driver?.Quit();
            _driver?.Dispose();
        }
    }
}
