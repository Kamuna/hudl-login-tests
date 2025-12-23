using OpenQA.Selenium;

namespace hudl_login_tests.Pages
{
    public class HudlHomePage : BasePage
    {
        private const string HomePageUrl = "https://www.hudl.com";

        public HudlHomePage(IWebDriver driver) : base(driver) { }

        private By LoginDropdown =>
            By.XPath("/html/body/header/div/div[2]/a");

        private By HudlLoginLink =>
            By.XPath("/html/body/header/div/div[2]/div/div/div/div/a[1]/span");

        public void Open(string url) => Driver.Navigate().GoToUrl(url);

        public void GoToHudlLogin()
        {
            ClickElement(LoginDropdown);
            ClickElement(HudlLoginLink);
        }

        public bool IsOnHomePage()
        {
            return Driver.Url.StartsWith(HomePageUrl) && !Driver.Url.Contains("/home");
        }
    }
}
