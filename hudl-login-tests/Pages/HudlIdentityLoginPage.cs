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

        private By InvalidPasswordError =>
            By.XPath("//*[@id='error-element-password']");

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
                Thread.Sleep(500); // Allow time for validation to appear
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
    }
}
