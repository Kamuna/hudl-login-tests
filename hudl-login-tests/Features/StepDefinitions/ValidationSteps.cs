using hudl_login_tests.Configuration;
using OpenQA.Selenium;

namespace hudl_login_tests.Features.StepDefinitions
{
    /// <summary>
    /// Step definitions for validation and error message assertions.
    /// </summary>
    public class ValidationSteps : BaseSteps
    {
        public ValidationSteps(IWebDriver driver) : base(driver) { }

        #region Input Actions

        /// <summary>
        /// Submits an empty email field.
        /// </summary>
        public async Task UserSubmitsEmptyEmail()
        {
            await ExecuteStep(nameof(UserSubmitsEmptyEmail), () =>
            {
                LoginPage.SubmitEmptyEmail();
            });
        }

        /// <summary>
        /// Submits an empty password field.
        /// </summary>
        public async Task UserSubmitsEmptyPassword()
        {
            await ExecuteStep(nameof(UserSubmitsEmptyPassword), () =>
            {
                LoginPage.EnterEmail(TestConfiguration.ValidUserEmail);
                LoginPage.ClickContinue();
                LoginPage.SubmitEmptyPassword();
            });
        }

        /// <summary>
        /// Enters excessively long input for boundary testing.
        /// </summary>
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

        #endregion

        #region Error Assertions

        /// <summary>
        /// Asserts a generic error message is displayed.
        /// </summary>
        public async Task AnErrorMessageShouldBeDisplayed()
        {
            await ExecuteStep(nameof(AnErrorMessageShouldBeDisplayed), () =>
            {
                AssertTrue(LoginPage.IsErrorDisplayed(), "An error message should be displayed for invalid credentials");
            });
        }

        /// <summary>
        /// Asserts a validation error is displayed.
        /// </summary>
        public async Task AValidationErrorShouldBeDisplayed()
        {
            await ExecuteStep(nameof(AValidationErrorShouldBeDisplayed), () =>
            {
                AssertTrue(LoginPage.IsValidationErrorDisplayed(), "A validation error should be displayed");
            });
        }

        /// <summary>
        /// Asserts invalid email error message is displayed.
        /// </summary>
        public async Task AnInvalidEmailErrorMessageShouldBeDisplayed()
        {
            await ExecuteStep(nameof(AnInvalidEmailErrorMessageShouldBeDisplayed), () =>
            {
                AssertTrue(LoginPage.IsInvalidEmailErrorDisplayed(), "An invalid email error message should be displayed");
                var errorText = LoginPage.GetInvalidEmailErrorText();
                AssertContains("Enter a valid email", errorText);
            });
        }

        /// <summary>
        /// Asserts email error message contains expected text.
        /// </summary>
        public async Task EmailErrorMessageShouldBeDisplayed(string expectedMessage)
        {
            await ExecuteStep($"EmailErrorMessageShouldBeDisplayed_{expectedMessage}", () =>
            {
                AssertTrue(LoginPage.IsEmailErrorDisplayed(), "An email error message should be displayed");
                var errorText = LoginPage.GetEmailErrorText();
                AssertContains(expectedMessage, errorText);
            });
        }

        /// <summary>
        /// Asserts invalid password error message is displayed.
        /// </summary>
        public async Task AnInvalidPasswordErrorMessageShouldBeDisplayed()
        {
            await ExecuteStep(nameof(AnInvalidPasswordErrorMessageShouldBeDisplayed), () =>
            {
                AssertTrue(LoginPage.IsInvalidPasswordErrorDisplayed(), "An invalid password error message should be displayed");
                var errorText = LoginPage.GetInvalidPasswordErrorText();
                AssertContains("Incorrect username or password", errorText);
            });
        }

        /// <summary>
        /// Asserts empty password error message is displayed.
        /// </summary>
        public async Task EmptyPasswordErrorMessageShouldBeDisplayed()
        {
            await ExecuteStep(nameof(EmptyPasswordErrorMessageShouldBeDisplayed), () =>
            {
                AssertTrue(LoginPage.IsEmptyPasswordErrorDisplayed(), "An empty password error message should be displayed");
                var errorText = LoginPage.GetEmptyPasswordErrorText();
                AssertContains("Enter your password", errorText);
            });
        }

        /// <summary>
        /// Asserts password field is masked.
        /// </summary>
        public async Task PasswordFieldShouldBeMasked()
        {
            await ExecuteStep(nameof(PasswordFieldShouldBeMasked), () =>
            {
                AssertTrue(LoginPage.IsPasswordMasked(), "Password field should be masked (type='password')");
            });
        }

        /// <summary>
        /// Asserts password field is visible (unmasked).
        /// </summary>
        public async Task PasswordFieldShouldBeVisible()
        {
            await ExecuteStep(nameof(PasswordFieldShouldBeVisible), () =>
            {
                AssertTrue(LoginPage.IsPasswordVisible(), "Password field should be visible (type='text')");
            });
        }

        /// <summary>
        /// Asserts application handles long input gracefully.
        /// </summary>
        public async Task ApplicationShouldHandleLongInputGracefully()
        {
            await ExecuteStep(nameof(ApplicationShouldHandleLongInputGracefully), () =>
            {
                AssertTrue(LoginPage.IsPageResponsive(), "Application should handle long input without crashing");
            });
        }

        #endregion
    }
}
