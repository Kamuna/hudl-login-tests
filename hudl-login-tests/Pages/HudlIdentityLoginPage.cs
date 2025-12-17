using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace hudl_login_tests.Pages
{
    public class HudlIdentityLoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public HudlIdentityLoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

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

        public void EnterEmail(string email)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(EmailInput));
            _driver.FindElement(EmailInput).Clear();
            _driver.FindElement(EmailInput).SendKeys(email);
        }

        public void ClickContinue()
        {
            _driver.FindElement(ContinueButton).Click();
        }

        public void ClickSubmit()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(SubmitLoginDetails));
            _driver.FindElement(SubmitLoginDetails).Click();
        }

        public void EnterPassword(string password)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(PasswordInput));
            _driver.FindElement(PasswordInput).Clear();
            _driver.FindElement(PasswordInput).SendKeys(password);
            _driver.FindElement(SubmitLoginDetails).Click();
        }

        public void Login(string email, string password)
        {
            EnterEmail(email);
            ClickContinue();
            EnterPassword(password);
        }

        public void SubmitEmptyEmail()
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(EmailInput));
            _driver.FindElement(EmailInput).Clear();
            _driver.FindElement(ContinueButton).Click();
        }

        public void SubmitEmptyPassword()
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(PasswordInput));
            _driver.FindElement(PasswordInput).Clear();
            _driver.FindElement(SubmitLoginDetails).Click();
        }

        public bool IsErrorDisplayed()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(ErrorMessage));
                return _driver.FindElement(ErrorMessage).Displayed;
            }
            catch { return false; }
        }

        public bool IsValidationErrorDisplayed()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(ValidationError));
                return _driver.FindElement(ValidationError).Displayed;
            }
            catch { return false; }
        }

        public bool IsInvalidEmailErrorDisplayed()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(InvalidEmailError));
                return _driver.FindElement(InvalidEmailError).Displayed;
            }
            catch { return false; }
        }

        public string GetInvalidEmailErrorText()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(InvalidEmailError));
                return _driver.FindElement(InvalidEmailError).Text;
            }
            catch { return string.Empty; }
        }

        public bool IsEmailErrorDisplayed()
        {
            try
            {
                var emptyError = _driver.FindElements(EmptyEmailError);
                var invalidError = _driver.FindElements(InvalidEmailError);

                if (emptyError.Count > 0 && emptyError[0].Displayed) return true;
                if (invalidError.Count > 0 && invalidError[0].Displayed) return true;

                return false;
            }
            catch { return false; }
        }

        public string GetEmailErrorText()
        {
            try
            {
                var emptyError = _driver.FindElements(EmptyEmailError);
                if (emptyError.Count > 0 && emptyError[0].Displayed)
                    return emptyError[0].Text;

                
                var invalidError = _driver.FindElements(InvalidEmailError);
                if (invalidError.Count > 0 && invalidError[0].Displayed)
                    return invalidError[0].Text;

                return string.Empty;
            }
            catch { return string.Empty; }
        }

        public bool IsInvalidPasswordErrorDisplayed()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(InvalidPasswordError));
                return _driver.FindElement(InvalidPasswordError).Displayed;
            }
            catch { return false; }
        }

        public string GetInvalidPasswordErrorText()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(InvalidPasswordError));
                return _driver.FindElement(InvalidPasswordError).Text;
            }
            catch { return string.Empty; }
        }

        public bool IsEmptyPasswordErrorDisplayed()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(EmptyPasswordError));
                return _driver.FindElement(EmptyPasswordError).Displayed;
            }
            catch { return false; }
        }

        public string GetEmptyPasswordErrorText()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(EmptyPasswordError));
                return _driver.FindElement(EmptyPasswordError).Text;
            }
            catch { return string.Empty; }
        }

        public void EnterPasswordOnly(string password)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(PasswordInput));
            _driver.FindElement(PasswordInput).Clear();
            _driver.FindElement(PasswordInput).SendKeys(password);
        }

        public void ClickPasswordVisibilityToggle()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(PasswordVisibilityToggle));
            _driver.FindElement(PasswordVisibilityToggle).Click();
        }

        public bool IsPasswordMasked()
        {
            var passwordField = _driver.FindElement(PasswordInput);
            return passwordField.GetAttribute("type") == "password";
        }

        public bool IsPasswordVisible()
        {
            var passwordField = _driver.FindElement(PasswordInput);
            return passwordField.GetAttribute("type") == "text";
        }

        public bool IsPageResponsive()
        {
            try
            {                
                var emailElements = _driver.FindElements(EmailInput);
                var passwordElements = _driver.FindElements(PasswordInput);

                return (emailElements.Count > 0 && emailElements[0].Displayed) ||
                       (passwordElements.Count > 0 && passwordElements[0].Displayed) ||
                       _driver.FindElements(By.TagName("body")).Count > 0;
            }
            catch
            {
                return false;
            }
        }

        public void ClickForgotPasswordLink()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(ForgotPasswordLink));
            _driver.FindElement(ForgotPasswordLink).Click();
        }

        public bool IsOnResetPasswordPage()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(ResetPasswordEmailInput));
                return _driver.FindElement(ResetPasswordEmailInput).Displayed;
            }
            catch { return false; }
        }

        public string GetResetPasswordEmailValue()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(ResetPasswordEmailInput));
                return _driver.FindElement(ResetPasswordEmailInput).GetAttribute("value") ?? string.Empty;
            }
            catch { return string.Empty; }
        }

        public void ClickResetPasswordContinue()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(ResetPasswordContinueButton));
            _driver.FindElement(ResetPasswordContinueButton).Click();
        }

        public bool IsOnCheckYourEmailPage()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(CheckYourEmailHeading));
                var heading = _driver.FindElement(CheckYourEmailHeading);
                return heading.Displayed && heading.Text.Contains("Check Your Email", StringComparison.OrdinalIgnoreCase);
            }
            catch { return false; }
        }

        public bool IsResendEmailButtonVisible()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(ResendEmailButton));
                return _driver.FindElement(ResendEmailButton).Displayed;
            }
            catch { return false; }
        }

        public void ClickEditEmailLink()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(EditEmailLink));
            _driver.FindElement(EditEmailLink).Click();
        }

        public bool IsOnEmailEntryScreen()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(EmailInput));
                return _driver.FindElement(EmailInput).Displayed;
            }
            catch { return false; }
        }
    }
}
