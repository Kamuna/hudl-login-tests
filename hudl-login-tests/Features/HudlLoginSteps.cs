using hudl_login_tests.Configuration;
using hudl_login_tests.Pages;
using hudl_login_tests.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


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
        private void AssertTrue(bool condition, string message)
        {
            Assert.True(condition, message);
        }

        private void AssertContains(string expectedSubstring, string actual)
        {
            Assert.Contains(expectedSubstring, actual);
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
                AssertTrue(_dashboard.IsOnHomePage(), $"User should be redirected to {TestConfiguration.HomePageUrl}");
            });
        }

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
                AssertTrue(_loginPage.IsErrorDisplayed(), "An error message should be displayed for invalid credentials");
            });
        }

        public async Task AnInvalidEmailErrorMessageShouldBeDisplayed()
        {
            await ExecuteStep(nameof(AnInvalidEmailErrorMessageShouldBeDisplayed), () =>
            {
                AssertTrue(_loginPage.IsInvalidEmailErrorDisplayed(), "An invalid email error message should be displayed");
                var errorText = _loginPage.GetInvalidEmailErrorText();
                AssertContains("Enter a valid email", errorText);
            });
        }

        public async Task EmailErrorMessageShouldBeDisplayed(string expectedMessage)
        {
            await ExecuteStep($"EmailErrorMessageShouldBeDisplayed_{expectedMessage}", () =>
            {
                AssertTrue(_loginPage.IsEmailErrorDisplayed(), "An email error message should be displayed");
                var errorText = _loginPage.GetEmailErrorText();
                AssertContains(expectedMessage, errorText);
            });
        }

        public async Task AnInvalidPasswordErrorMessageShouldBeDisplayed()
        {
            await ExecuteStep(nameof(AnInvalidPasswordErrorMessageShouldBeDisplayed), () =>
            {
                AssertTrue(_loginPage.IsInvalidPasswordErrorDisplayed(), "An invalid password error message should be displayed");
                var errorText = _loginPage.GetInvalidPasswordErrorText();
                AssertContains(TestConfiguration.ExpectedInvalidCredentialsMessage, errorText);
            });
        }

        public async Task EmptyPasswordErrorMessageShouldBeDisplayed()
        {
            await ExecuteStep(nameof(EmptyPasswordErrorMessageShouldBeDisplayed), () =>
            {
                AssertTrue(_loginPage.IsEmptyPasswordErrorDisplayed(), "An empty password error message should be displayed");
                var errorText = _loginPage.GetEmptyPasswordErrorText();
                AssertContains(TestConfiguration.ExpectedEmptyPasswordMessage, errorText);
            });
        }


        public async Task UserEntersExcessivelyLongInput(string field, int length)
        {
            await ExecuteStep($"UserEntersExcessivelyLongInput_{field}_{length}", () =>
            {
                var longValue = new string('a', length);

                if (field.ToLower() == "email")
                {
                    _loginPage.EnterEmail(longValue + TestConfiguration.EmailDomainSuffix);
                    _loginPage.ClickContinue();
                }
                else if (field.ToLower() == "password")
                {
                    _loginPage.EnterEmail(TestConfiguration.ValidUserEmail);
                    _loginPage.ClickContinue();
                    _loginPage.EnterPasswordOnly(longValue);
                }
            });
        }

        public async Task ApplicationShouldHandleLongInputGracefully()
        {
            await ExecuteStep(nameof(ApplicationShouldHandleLongInputGracefully), () =>
            {
                AssertTrue(_loginPage.IsPageResponsive(), "Application should handle long input without crashing");
            });
        }


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
                _loginPage.ClickContinue();
                _loginPage.SubmitEmptyPassword();
            });
        }

        public async Task AValidationErrorShouldBeDisplayed()
        {
            await ExecuteStep(nameof(AValidationErrorShouldBeDisplayed), () =>
            {
                AssertTrue(_loginPage.IsValidationErrorDisplayed(), "A validation error should be displayed");
            });
        }


        public async Task UserEntersEmailAndProceedsToPasswordScreen()
        {
            await ExecuteStep(nameof(UserEntersEmailAndProceedsToPasswordScreen), () =>
            {
                _loginPage.EnterEmail(TestConfiguration.ValidUserEmail);
                _loginPage.ClickContinue();
            });
        }

        public async Task UserEntersPasswordOnScreen()
        {
            await ExecuteStep(nameof(UserEntersPasswordOnScreen), () =>
            {
                _loginPage.EnterPasswordOnly(TestConfiguration.SamplePassword);
            });
        }

        public async Task UserClicksPasswordVisibilityToggle()
        {
            await ExecuteStep(nameof(UserClicksPasswordVisibilityToggle), () =>
            {
                _loginPage.ClickPasswordVisibilityToggle();
            });
        }

        public async Task PasswordFieldShouldBeMasked()
        {
            await ExecuteStep(nameof(PasswordFieldShouldBeMasked), () =>
            {
                AssertTrue(_loginPage.IsPasswordMasked(), "Password field should be masked (type='password')");
            });
        }

        public async Task PasswordFieldShouldBeVisible()
        {
            await ExecuteStep(nameof(PasswordFieldShouldBeVisible), () =>
            {
                AssertTrue(_loginPage.IsPasswordVisible(), "Password field should be visible (type='text')");
            });
        }

        public async Task UserClicksLogout()
        {
            await ExecuteStep(nameof(UserClicksLogout), () =>
            {
                _dashboard.HoverOverProfileMenu();
                _dashboard.ClickLogout();
            });
        }

        public async Task UserShouldBeLoggedOut()
        {
            await ExecuteStep(nameof(UserShouldBeLoggedOut), () =>
            {
                AssertTrue(_dashboard.IsOnLoggedOutPage(), $"User should be redirected to {TestConfiguration.LoggedOutUrl} after logout");
            });
        }


        public async Task UserPressesBackButton()
        {
            await ExecuteStep(nameof(UserPressesBackButton), () =>
            {
                _driver.Navigate().Back();
            });
        }

        public async Task UserHoversOverProfileAndClicksAccountSettings()
        {
            await ExecuteStep(nameof(UserHoversOverProfileAndClicksAccountSettings), () =>
            {
                _dashboard.HoverOverProfileMenu();
                _dashboard.ClickAccountSettings();
            });
        }

        public async Task UserShouldBeRedirectedToLoginPage()
        {
            await ExecuteStep(nameof(UserShouldBeRedirectedToLoginPage), () =>
            {
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.Url.Contains(TestConfiguration.LoginUrlPattern) || d.Url.Contains("login"));

                var currentUrl = _driver.Url;
                AssertTrue(currentUrl.Contains(TestConfiguration.LoginUrlPattern) && currentUrl.Contains("login"),
                    $"User should be redirected to identity login page. Current URL: {currentUrl}");
            });
        }

        public async Task UserLoginsWithEmail(string email)
        {
            await ExecuteStep($"UserLoginsWithEmail_{email}", () =>
            {
                _loginPage.Login(email, TestConfiguration.ValidUserPassword);
            });
        }

        public async Task UserEntersEmail(string email)
        {
            await ExecuteStep(nameof(UserEntersEmail), () =>
            {
                _loginPage.EnterEmail(email);
            });
        }

        public async Task UserClicksContinue()
        {
            await ExecuteStep(nameof(UserClicksContinue), () =>
            {
                _loginPage.ClickContinue();
            });
        }

        public async Task UserClicksEditEmailLink()
        {
            await ExecuteStep(nameof(UserClicksEditEmailLink), () =>
            {
                _loginPage.ClickEditEmailLink();
            });
        }

        public async Task UserShouldBeOnEmailEntryScreen()
        {
            await ExecuteStep(nameof(UserShouldBeOnEmailEntryScreen), () =>
            {
                AssertTrue(_loginPage.IsOnEmailEntryScreen(),
                    "User should be back on the email entry screen after clicking edit email link");
            });
        }

        public async Task UserEntersPassword(string password)
        {
            await ExecuteStep(nameof(UserEntersPassword), () =>
            {
                _loginPage.EnterPasswordOnly(password);
            });
        }

        public async Task UserClicksSubmit()
        {
            await ExecuteStep(nameof(UserClicksSubmit), () =>
            {
                _loginPage.ClickSubmit();
            });
        }

        public async Task UserClicksForgotPasswordLink()
        {
            await ExecuteStep(nameof(UserClicksForgotPasswordLink), () =>
            {
                _loginPage.ClickForgotPasswordLink();
            });
        }

        public async Task UserShouldBeOnResetPasswordPage()
        {
            await ExecuteStep(nameof(UserShouldBeOnResetPasswordPage), () =>
            {
                AssertTrue(_loginPage.IsOnResetPasswordPage(),
                    "User should be on the reset password page");
            });
        }

        public async Task EmailFieldShouldBePrePopulatedWith(string expectedEmail)
        {
            await ExecuteStep(nameof(EmailFieldShouldBePrePopulatedWith), () =>
            {
                var actualEmail = _loginPage.GetResetPasswordEmailValue();
                AssertTrue(actualEmail.Equals(expectedEmail, StringComparison.OrdinalIgnoreCase),
                    $"Email field should be pre-populated with '{expectedEmail}', but was '{actualEmail}'");
            });
        }

        public async Task UserClicksResetPasswordContinue()
        {
            await ExecuteStep(nameof(UserClicksResetPasswordContinue), () =>
            {
                _loginPage.ClickResetPasswordContinue();
            });
        }

        public async Task UserShouldSeeCheckYourEmailPage()
        {
            await ExecuteStep(nameof(UserShouldSeeCheckYourEmailPage), () =>
            {
                AssertTrue(_loginPage.IsOnCheckYourEmailPage(),
                    "User should see the 'Check Your Email' confirmation page");
            });
        }
        public async Task ResendEmailLinkShouldBeVisible()
        {
            await ExecuteStep(nameof(ResendEmailLinkShouldBeVisible), () =>
            {
                AssertTrue(_loginPage.IsResendEmailButtonVisible(),
                    "Resend email button should be visible on the confirmation page");
            });
        }

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