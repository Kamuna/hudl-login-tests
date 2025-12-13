using OpenQA.Selenium;

namespace hudl_login_tests.Pages
{
    public class HudlHomePage
    {
        private readonly IWebDriver _driver;
        private const string HomePageUrl = "https://www.hudl.com";

        public HudlHomePage(IWebDriver driver) => _driver = driver;

        private By LoginDropdown =>
            By.XPath("/html/body/header/div/div[2]/a");

        private By HudlLoginLink =>
            By.XPath("/html/body/header/div/div[2]/div/div/div/div/a[1]/span");

        public void Open(string url) => _driver.Navigate().GoToUrl(url);

        public void GoToHudlLogin()
        {
            _driver.FindElement(LoginDropdown).Click();
            _driver.FindElement(HudlLoginLink).Click();
        }

        public bool IsOnHomePage()
        {
            return _driver.Url.StartsWith(HomePageUrl) && !_driver.Url.Contains("/home");
        }
    }
}
