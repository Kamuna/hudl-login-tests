using hudl_login_tests.Configuration;
using OpenQA.Selenium;

namespace hudl_login_tests.Features.StepDefinitions
{
    public class LoginSteps : BaseSteps
    {
        public LoginSteps(IWebDriver driver) : base(driver) { }

        public async Task AUserNavigatesToTheLoginPage()
        {
            await ExecuteStep(nameof(AUserNavigatesToTheLoginPage), () =>
            {
                HomePage.Open(TestConfiguration.HomepageUrl);
                HomePage.GoToHudlLogin();
            });
        }

        public async Task UserLoginsInWithValidCredentials()
        {
            await ExecuteStep(nameof(UserLoginsInWithValidCredentials), () =>
            {
                LoginPage.Login(TestConfiguration.ValidUserEmail, TestConfiguration.ValidUserPassword);
            });
        }

        public async Task UserLoginsInWithInvalidPassword()
        {
            await ExecuteStep(nameof(UserLoginsInWithInvalidPassword), () =>
            {
                LoginPage.Login(TestConfiguration.InvalidPasswordUserEmail, TestConfiguration.InvalidPasswordUserPassword);
            });
        }

        public async Task UserLoginsWithEmail(string email)
        {
            await ExecuteStep($"UserLoginsWithEmail_{email}", () =>
            {
                LoginPage.Login(email, TestConfiguration.ValidUserPassword);
            });
        }
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
        public async Task UserEntersEmail(string email)
        {
            await ExecuteStep(nameof(UserEntersEmail), () =>
            {
                LoginPage.EnterEmail(email);
            });
        }
        public async Task UserClicksContinue()
        {
            await ExecuteStep(nameof(UserClicksContinue), () =>
            {
                LoginPage.ClickContinue();
            });
        }
        public async Task UserEntersEmailAndProceedsToPasswordScreen()
        {
            await ExecuteStep(nameof(UserEntersEmailAndProceedsToPasswordScreen), () =>
            {
                LoginPage.EnterEmail(TestConfiguration.ValidUserEmail);
                LoginPage.ClickContinue();
            });
        }
        public async Task UserEntersPasswordOnScreen()
        {
            await ExecuteStep(nameof(UserEntersPasswordOnScreen), () =>
            {
                LoginPage.EnterPasswordOnly("TestPassword123");
            });
        }
        public async Task UserEntersPassword(string password)
        {
            await ExecuteStep(nameof(UserEntersPassword), () =>
            {
                LoginPage.EnterPasswordOnly(password);
            });
        }
        public async Task UserClicksSubmit()
        {
            await ExecuteStep(nameof(UserClicksSubmit), () =>
            {
                LoginPage.ClickSubmit();
            });
        }

        public async Task UserClicksEditEmailLink()
        {
            await ExecuteStep(nameof(UserClicksEditEmailLink), () =>
            {
                LoginPage.ClickEditEmailLink();
            });
        }

        public async Task UserClicksPasswordVisibilityToggle()
        {
            await ExecuteStep(nameof(UserClicksPasswordVisibilityToggle), () =>
            {
                LoginPage.ClickPasswordVisibilityToggle();
            });
        }

        public async Task UserClicksLogout()
        {
            await ExecuteStep(nameof(UserClicksLogout), () =>
            {
                Dashboard.HoverOverProfileMenu();
                Dashboard.ClickLogout();
            });
        }
        public async Task UserShouldBeLoggedInSuccessfully()
        {
            await ExecuteStep(nameof(UserShouldBeLoggedInSuccessfully), () =>
            {
                AssertTrue(Dashboard.IsOnHomePage(), "User should be redirected to https://www.hudl.com/home");
            });
        }

        public async Task UserShouldBeLoggedOut()
        {
            await ExecuteStep(nameof(UserShouldBeLoggedOut), () =>
            {
                AssertTrue(Dashboard.IsOnLoggedOutPage(), "User should be redirected to https://www.hudl.com/en_gb/ after logout");
            });
        }

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
