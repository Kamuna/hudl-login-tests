using hudl_login_tests.Configuration;
using OpenQA.Selenium;

namespace hudl_login_tests.Features.StepDefinitions
{
    /// <summary>
    /// Step definitions for core login functionality.
    /// </summary>
    public class LoginSteps : BaseSteps
    {
        public LoginSteps(IWebDriver driver) : base(driver) { }

        /// <summary>
        /// Navigates to the Hudl login page.
        /// </summary>
        public async Task AUserNavigatesToTheLoginPage()
        {
            await ExecuteStep(nameof(AUserNavigatesToTheLoginPage), () =>
            {
                HomePage.Open(TestConfiguration.HomepageUrl);
                HomePage.GoToHudlLogin();
            });
        }

        /// <summary>
        /// Logs in with valid credentials from configuration.
        /// </summary>
        public async Task UserLoginsInWithValidCredentials()
        {
            await ExecuteStep(nameof(UserLoginsInWithValidCredentials), () =>
            {
                LoginPage.Login(TestConfiguration.ValidUserEmail, TestConfiguration.ValidUserPassword);
            });
        }

        /// <summary>
        /// Logs in with invalid password credentials.
        /// </summary>
        public async Task UserLoginsInWithInvalidPassword()
        {
            await ExecuteStep(nameof(UserLoginsInWithInvalidPassword), () =>
            {
                LoginPage.Login(TestConfiguration.InvalidPasswordUserEmail, TestConfiguration.InvalidPasswordUserPassword);
            });
        }

        /// <summary>
        /// Logs in with a specific email address.
        /// </summary>
        public async Task UserLoginsWithEmail(string email)
        {
            await ExecuteStep($"UserLoginsWithEmail_{email}", () =>
            {
                LoginPage.Login(email, TestConfiguration.ValidUserPassword);
            });
        }

        /// <summary>
        /// Enters email and clicks continue.
        /// </summary>
        public async Task UserEntersEmailAndClicksContinue(string email)
        {
            await ExecuteStep($"UserEntersEmailAndClicksContinue_{email}", () =>
            {
                if (string.IsNullOrEmpty(email))
                {
                    LoginPage.SubmitEmptyEmail();
                }
                else
                {
                    LoginPage.EnterEmail(email);
                    LoginPage.ClickContinue();
                }
            });
        }

        /// <summary>
        /// Enters email address only.
        /// </summary>
        public async Task UserEntersEmail(string email)
        {
            await ExecuteStep(nameof(UserEntersEmail), () =>
            {
                LoginPage.EnterEmail(email);
            });
        }

        /// <summary>
        /// Clicks the continue button.
        /// </summary>
        public async Task UserClicksContinue()
        {
            await ExecuteStep(nameof(UserClicksContinue), () =>
            {
                LoginPage.ClickContinue();
            });
        }

        /// <summary>
        /// Enters email and proceeds to password screen.
        /// </summary>
        public async Task UserEntersEmailAndProceedsToPasswordScreen()
        {
            await ExecuteStep(nameof(UserEntersEmailAndProceedsToPasswordScreen), () =>
            {
                LoginPage.EnterEmail(TestConfiguration.ValidUserEmail);
                LoginPage.ClickContinue();
            });
        }

        /// <summary>
        /// Enters password on the password screen.
        /// </summary>
        public async Task UserEntersPasswordOnScreen()
        {
            await ExecuteStep(nameof(UserEntersPasswordOnScreen), () =>
            {
                LoginPage.EnterPasswordOnly("TestPassword123");
            });
        }

        /// <summary>
        /// Enters a specific password.
        /// </summary>
        public async Task UserEntersPassword(string password)
        {
            await ExecuteStep(nameof(UserEntersPassword), () =>
            {
                LoginPage.EnterPasswordOnly(password);
            });
        }

        /// <summary>
        /// Clicks the submit/login button.
        /// </summary>
        public async Task UserClicksSubmit()
        {
            await ExecuteStep(nameof(UserClicksSubmit), () =>
            {
                LoginPage.ClickSubmit();
            });
        }

        /// <summary>
        /// Clicks the edit email link on password screen.
        /// </summary>
        public async Task UserClicksEditEmailLink()
        {
            await ExecuteStep(nameof(UserClicksEditEmailLink), () =>
            {
                LoginPage.ClickEditEmailLink();
            });
        }

        /// <summary>
        /// Toggles password visibility.
        /// </summary>
        public async Task UserClicksPasswordVisibilityToggle()
        {
            await ExecuteStep(nameof(UserClicksPasswordVisibilityToggle), () =>
            {
                LoginPage.ClickPasswordVisibilityToggle();
            });
        }

        /// <summary>
        /// Clicks logout from the profile menu.
        /// </summary>
        public async Task UserClicksLogout()
        {
            await ExecuteStep(nameof(UserClicksLogout), () =>
            {
                Dashboard.HoverOverProfileMenu();
                Dashboard.ClickLogout();
            });
        }

        /// <summary>
        /// Asserts user is logged in successfully.
        /// </summary>
        public async Task UserShouldBeLoggedInSuccessfully()
        {
            await ExecuteStep(nameof(UserShouldBeLoggedInSuccessfully), () =>
            {
                AssertTrue(Dashboard.IsOnHomePage(), "User should be redirected to https://www.hudl.com/home");
            });
        }

        /// <summary>
        /// Asserts user is logged out.
        /// </summary>
        public async Task UserShouldBeLoggedOut()
        {
            await ExecuteStep(nameof(UserShouldBeLoggedOut), () =>
            {
                AssertTrue(Dashboard.IsOnLoggedOutPage(), "User should be redirected to https://www.hudl.com/en_gb/ after logout");
            });
        }

        /// <summary>
        /// Asserts user is on email entry screen.
        /// </summary>
        public async Task UserShouldBeOnEmailEntryScreen()
        {
            await ExecuteStep(nameof(UserShouldBeOnEmailEntryScreen), () =>
            {
                AssertTrue(LoginPage.IsOnEmailEntryScreen(),
                    "User should be back on the email entry screen after clicking edit email link");
            });
        }
    }
}
