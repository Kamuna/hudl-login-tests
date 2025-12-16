using hudl_login_tests.Configuration;
using OpenQA.Selenium;

namespace hudl_login_tests.Features.StepDefinitions
{
    public class ValidationSteps : BaseSteps
    {
        public ValidationSteps(IWebDriver driver) : base(driver) { }

 
        public async Task UserSubmitsEmptyEmail()
        {
            await ExecuteStep(nameof(UserSubmitsEmptyEmail), () =>
            {
                LoginPage.SubmitEmptyEmail();
            });
        }

      
        public async Task UserSubmitsEmptyPassword()
        {
            await ExecuteStep(nameof(UserSubmitsEmptyPassword), () =>
            {
                LoginPage.EnterEmail(TestConfiguration.ValidUserEmail);
                LoginPage.ClickContinue();
                LoginPage.SubmitEmptyPassword();
            });
        }

       
        public async Task UserEntersExcessivelyLongInput(string field, int length)
        {
            await ExecuteStep($"UserEntersExcessivelyLongInput_{field}_{length}", () =>
            {
                var longValue = new string('a', length);

                if (field.ToLower() == "email")
                {
                    LoginPage.EnterEmail(longValue + "@test.com");
                    LoginPage.ClickContinue();
                }
                else if (field.ToLower() == "password")
                {
                    LoginPage.EnterEmail(TestConfiguration.ValidUserEmail);
                    LoginPage.ClickContinue();
                    LoginPage.EnterPasswordOnly(longValue);
                }
            });
        }

     

        public async Task AnErrorMessageShouldBeDisplayed()
        {
            await ExecuteStep(nameof(AnErrorMessageShouldBeDisplayed), () =>
            {
                AssertTrue(LoginPage.IsErrorDisplayed(), "An error message should be displayed for invalid credentials");
            });
        }

        
        public async Task AValidationErrorShouldBeDisplayed()
        {
            await ExecuteStep(nameof(AValidationErrorShouldBeDisplayed), () =>
            {
                AssertTrue(LoginPage.IsValidationErrorDisplayed(), "A validation error should be displayed");
            });
        }

        
        public async Task AnInvalidEmailErrorMessageShouldBeDisplayed()
        {
            await ExecuteStep(nameof(AnInvalidEmailErrorMessageShouldBeDisplayed), () =>
            {
                AssertTrue(LoginPage.IsInvalidEmailErrorDisplayed(), "An invalid email error message should be displayed");
                var errorText = LoginPage.GetInvalidEmailErrorText();
                AssertContains("Enter a valid email", errorText);
            });
        }

      
        public async Task EmailErrorMessageShouldBeDisplayed(string expectedMessage)
        {
            await ExecuteStep($"EmailErrorMessageShouldBeDisplayed_{expectedMessage}", () =>
            {
                AssertTrue(LoginPage.IsEmailErrorDisplayed(), "An email error message should be displayed");
                var errorText = LoginPage.GetEmailErrorText();
                AssertContains(expectedMessage, errorText);
            });
        }

        
        public async Task AnInvalidPasswordErrorMessageShouldBeDisplayed()
        {
            await ExecuteStep(nameof(AnInvalidPasswordErrorMessageShouldBeDisplayed), () =>
            {
                AssertTrue(LoginPage.IsInvalidPasswordErrorDisplayed(), "An invalid password error message should be displayed");
                var errorText = LoginPage.GetInvalidPasswordErrorText();
                AssertContains("Incorrect username or password", errorText);
            });
        }

      
        public async Task EmptyPasswordErrorMessageShouldBeDisplayed()
        {
            await ExecuteStep(nameof(EmptyPasswordErrorMessageShouldBeDisplayed), () =>
            {
                AssertTrue(LoginPage.IsEmptyPasswordErrorDisplayed(), "An empty password error message should be displayed");
                var errorText = LoginPage.GetEmptyPasswordErrorText();
                AssertContains("Enter your password", errorText);
            });
        }

      
        public async Task PasswordFieldShouldBeMasked()
        {
            await ExecuteStep(nameof(PasswordFieldShouldBeMasked), () =>
            {
                AssertTrue(LoginPage.IsPasswordMasked(), "Password field should be masked (type='password')");
            });
        }

       
        public async Task PasswordFieldShouldBeVisible()
        {
            await ExecuteStep(nameof(PasswordFieldShouldBeVisible), () =>
            {
                AssertTrue(LoginPage.IsPasswordVisible(), "Password field should be visible (type='text')");
            });
        }

        public async Task ApplicationShouldHandleLongInputGracefully()
        {
            await ExecuteStep(nameof(ApplicationShouldHandleLongInputGracefully), () =>
            {
                AssertTrue(LoginPage.IsPageResponsive(), "Application should handle long input without crashing");
            });
        }
    }
}
