using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace hudl_login_tests.Pages
{
    public class HudlAccountSettingsPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public HudlAccountSettingsPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        private const string AccountSettingsUrlPattern = "hudl.com/profile";

        private By ResetPasswordButton => By.XPath("//*[@id='resetPassword']");
        private By SuccessToast => By.XPath("//*[@id='SuccessToast']/div[2]");

        public bool IsOnAccountSettingsPage()
        {
            try
            {
                _wait.Until(d => d.Url.Contains(AccountSettingsUrlPattern));
                return _driver.Url.Contains(AccountSettingsUrlPattern);
            }
            catch { return false; }
        }

        public void ClickResetPassword()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(ResetPasswordButton));
            var resetButton = _driver.FindElement(ResetPasswordButton);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", resetButton);
        }

        public bool IsSuccessToastDisplayed()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(SuccessToast));
                return _driver.FindElement(SuccessToast).Displayed;
            }
            catch { return false; }
        }

        public string GetSuccessToastText()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(SuccessToast));
                return _driver.FindElement(SuccessToast).Text;
            }
            catch { return string.Empty; }
        }
    }
}
