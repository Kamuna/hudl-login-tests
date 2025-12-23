using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace hudl_login_tests.Pages
{
    public class HudlIdentityLoginPage : BasePage
    {
        public HudlIdentityLoginPage(IWebDriver driver) : base(driver) { }

        private By EmailInput =>
            By.XPath("//*[@id=\"username\"]");

        private By ContinueButton =>
            By.XPath("/html/body/code/div/main/section/div/div/div/div[1]/div/form/div[2]/button");

        private By SubmitLoginDetails =>
            By.XPath("/html/body/code/div/main/section/div/div/div/form/div[2]/button");

        private By PasswordInput =>
            By.XPath("//*[@id=\"password\"]");

        private By ErrorMessage =>
            By.XPath("//*[contains(text(),'incorrect') or contains(text(),'invalid') or contains(text(),'error') or contains(text(),'wrong')]");

        private By InvalidEmailError =>
            By.XPath("//*[@id='error-cs-email-invalid']");

        private By EmptyEmailError =>
            By.XPath("//*[@id='error-cs-username-required']");

        private By InvalidPasswordError =>
            By.XPath("//*[@id='error-element-password']");

        private By EmptyPasswordError =>
            By.XPath("//*[@id='error-cs-password-required']");

        private By PasswordVisibilityToggle =>
            By.XPath("/html/body/code/div/main/section/div/div/div/form/div[1]/div/div[2]/div[1]/button");

        private By EditEmailLink =>
            By.XPath("/html/body/code/div/main/section/div/div/div/form/div[1]/div/div[1]/div/a");

        private By ForgotPasswordLink =>
            By.XPath("/html/body/code/div/main/section/div/div/div/form/p/a");

        private By ResetPasswordEmailInput =>
            By.XPath("//*[@id='email']");

        private By ResetPasswordContinueButton =>
            By.XPath("/html/body/code/div/main/section/div/div/div/form/div[2]/button");

        private By CheckYourEmailHeading =>
            By.XPath("/html/body/code/div/main/section/div/div/section/h1");

        private By ResendEmailButton =>
            By.XPath("/html/body/code/div/main/section/div/div/section/form/button");

        private By ValidationError =>
            By.XPath("//*[@id='error-cs-email-invalid'] | //*[contains(@class,'error') or contains(@class,'invalid')] | //input[@aria-invalid='true']");

        public void EnterEmail(string email) => EnterText(EmailInput, email);

        public void ClickContinue() => ClickElement(ContinueButton);

        public void ClickSubmit() => ClickElement(SubmitLoginDetails);

        public void EnterPassword(string password)
        {
            EnterText(PasswordInput, password);
            ClickElement(SubmitLoginDetails);
        }

        public void Login(string email, string password)
        {
            EnterEmail(email);
            ClickContinue();
            EnterPassword(password);
        }

        public void SubmitEmptyEmail()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(EmailInput));
            Driver.FindElement(EmailInput).Clear();
            ClickElement(ContinueButton);
        }

        public void SubmitEmptyPassword()
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(PasswordInput));
            Driver.FindElement(PasswordInput).Clear();
            ClickElement(SubmitLoginDetails);
        }

        public bool IsErrorDisplayed() => IsElementVisible(ErrorMessage);

        public bool IsValidationErrorDisplayed() => IsElementVisible(ValidationError);

        public bool IsInvalidEmailErrorDisplayed() => IsElementVisible(InvalidEmailError);

        public string GetInvalidEmailErrorText() => GetElementText(InvalidEmailError);

        public bool IsEmailErrorDisplayed()
        {
            try
            {
                var emptyError = FindElements(EmptyEmailError);
                var invalidError = FindElements(InvalidEmailError);

                if (emptyError.Count > 0 && emptyError.First().Displayed) return true;
                if (invalidError.Count > 0 && invalidError.First().Displayed) return true;

                return false;
            }
            catch { return false; }
        }

        public string GetEmailErrorText()
        {
            try
            {
                var emptyError = FindElements(EmptyEmailError);
                if (emptyError.Count > 0 && emptyError.First().Displayed)
                    return emptyError.First().Text;

                var invalidError = FindElements(InvalidEmailError);
                if (invalidError.Count > 0 && invalidError.First().Displayed)
                    return invalidError.First().Text;

                return string.Empty;
            }
            catch { return string.Empty; }
        }

        public bool IsInvalidPasswordErrorDisplayed() => IsElementVisible(InvalidPasswordError);

        public string GetInvalidPasswordErrorText() => GetElementText(InvalidPasswordError);

        public bool IsEmptyPasswordErrorDisplayed() => IsElementVisible(EmptyPasswordError);

        public string GetEmptyPasswordErrorText() => GetElementText(EmptyPasswordError);

        public void EnterPasswordOnly(string password) => EnterText(PasswordInput, password);

        public void ClickPasswordVisibilityToggle() => ClickElement(PasswordVisibilityToggle);

        public bool IsPasswordMasked() => GetElementAttribute(PasswordInput, "type") == "password";

        public bool IsPasswordVisible() => GetElementAttribute(PasswordInput, "type") == "text";

        public bool IsPageResponsive()
        {
            try
            {
                var emailElements = FindElements(EmailInput);
                var passwordElements = FindElements(PasswordInput);

                return (emailElements.Count > 0 && emailElements.First().Displayed) ||
                       (passwordElements.Count > 0 && passwordElements.First().Displayed) ||
                       FindElements(By.TagName("body")).Count > 0;
            }
            catch { return false; }
        }

        public void ClickForgotPasswordLink() => ClickElement(ForgotPasswordLink);

        public bool IsOnResetPasswordPage() => IsElementVisible(ResetPasswordEmailInput);

        public string GetResetPasswordEmailValue() => GetElementAttribute(ResetPasswordEmailInput, "value");

        public void ClickResetPasswordContinue() => ClickElement(ResetPasswordContinueButton);

        public bool IsOnCheckYourEmailPage()
        {
            try
            {
                var headingText = GetElementText(CheckYourEmailHeading);
                return headingText.Contains("Check Your Email", StringComparison.OrdinalIgnoreCase);
            }
            catch { return false; }
        }

        public bool IsResendEmailButtonVisible() => IsElementVisible(ResendEmailButton);

        public void ClickEditEmailLink() => ClickElement(EditEmailLink);

        public bool IsOnEmailEntryScreen() => IsElementVisible(EmailInput);
    }
}
