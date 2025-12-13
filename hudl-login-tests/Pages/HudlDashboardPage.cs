using OpenQA.Selenium;
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

        private By ProfileMenuDropdown =>
            By.XPath("//*[@id='ssr-webnav']//div[@data-qa-id='webnav-usermenu-trigger'] | //*[@id='ssr-webnav']/div/div[1]/nav[1]/div[4]/div[3]/div[1]/div[2] | //div[contains(@class,'hui-globalusermenu')]//div[contains(@class,'trigger')]");

        private By LogOutLink =>
            By.XPath("//*[@id='ssr-webnav']/div/div[1]/nav[1]/div[4]/div[3]/div[2]/div[3]/a");

        public bool IsOnHomePage()
        {
            try
            {
                _wait.Until(d => d.Url.Contains(ExpectedUrl));
                return _driver.Url.StartsWith(ExpectedUrl);
            }
            catch { return false; }
        }

        public void ClickProfileMenu()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(ProfileMenuDropdown));
            _driver.FindElement(ProfileMenuDropdown).Click();
            // Wait a moment for the dropdown to appear
            Thread.Sleep(500);
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

        public void ClickLogout()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(LogOutLink));
            _driver.FindElement(LogOutLink).Click();
        }
    }
}
