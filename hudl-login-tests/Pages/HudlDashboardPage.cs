using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace hudl_login_tests.Pages
{
    public class HudlDashboardPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public HudlDashboardPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private const string ExpectedUrl = "https://www.hudl.com/home";
        private const string LoggedOutUrl = "https://www.hudl.com/en_gb/";

        private By ProfileMenuDropdown =>
            By.XPath("//*[@id='ssr-webnav']/div/div[1]/nav[1]/div[4]/div[3]/div[1]/div[2]");

        private By LogOutLink =>
            By.XPath("//*[@id='ssr-webnav']/div/div[1]/nav[1]/div[4]/div[3]/div[2]/div[3]/a/span");

        private By AccountSettingsLink =>
            By.XPath("//*[@id='ssr-webnav']/div/div[1]/nav[1]/div[4]/div[3]/div[2]/div[2]/a[1]");

        public bool IsOnHomePage()
        {
            try
            {
                _wait.Until(d => d.Url.Contains(ExpectedUrl));
                return _driver.Url.StartsWith(ExpectedUrl);
            }
            catch { return false; }
        }

        public bool IsOnLoggedOutPage()
        {
            try
            {
                _wait.Until(d => d.Url.StartsWith(LoggedOutUrl));
                return _driver.Url.StartsWith(LoggedOutUrl);
            }
            catch { return false; }
        }

        public void HoverOverProfileMenu()
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(ProfileMenuDropdown));
            var profileElement = _driver.FindElement(ProfileMenuDropdown);
            var actions = new Actions(_driver);
            actions.MoveToElement(profileElement).Perform();
            _wait.Until(ExpectedConditions.ElementIsVisible(LogOutLink));
        }

        public void ClickProfileMenu()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(ProfileMenuDropdown));
            _driver.FindElement(ProfileMenuDropdown).Click();
            _wait.Until(ExpectedConditions.ElementIsVisible(LogOutLink));
        }

        public bool IsLogOutVisible()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(LogOutLink));
                return _driver.FindElement(LogOutLink).Displayed;
            }
            catch { return false; }
        }

        public void ClickAccountSettings()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(AccountSettingsLink));
            var accountSettingsElement = _driver.FindElement(AccountSettingsLink);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", accountSettingsElement);
        }

        public void ClickLogout()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(LogOutLink));
            var logoutElement = _driver.FindElement(LogOutLink);
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", logoutElement);
        }
    }
}
