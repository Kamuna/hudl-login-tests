using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace hudl_login_tests.Pages
{
    public class HudlDashboardPage : BasePage
    {
        public HudlDashboardPage(IWebDriver driver) : base(driver) { }

        private const string ExpectedUrl = "https://www.hudl.com/home";
        private const string LoggedOutUrl = "https://www.hudl.com/en_gb/";

        private By ProfileMenuDropdown =>
            By.XPath("//*[@id='ssr-webnav']/div/div[1]/nav[1]/div[4]/div[3]/div[1]/div[2]");

        private By LogOutLink =>
            By.XPath("//*[@id='ssr-webnav']/div/div[1]/nav[1]/div[4]/div[3]/div[2]/div[3]/a/span");

        private By AccountSettingsLink =>
            By.XPath("//*[@id='ssr-webnav']/div/div[1]/nav[1]/div[4]/div[3]/div[2]/div[2]/a[1]");

        public bool IsOnHomePage() => WaitForUrlContains(ExpectedUrl);

        public bool IsOnLoggedOutPage() => WaitForUrlStartsWith(LoggedOutUrl);

        public void HoverOverProfileMenu()
        {
            HoverOverElement(ProfileMenuDropdown);
            Wait.Until(ExpectedConditions.ElementIsVisible(LogOutLink));
        }

        public void ClickProfileMenu()
        {
            ClickElement(ProfileMenuDropdown);
            Wait.Until(ExpectedConditions.ElementIsVisible(LogOutLink));
        }

        public bool IsLogOutVisible() => IsElementVisible(LogOutLink);

        public void ClickAccountSettings() => ClickWithJavaScript(AccountSettingsLink);

        public void ClickLogout() => ClickWithJavaScript(LogOutLink);
    }
}
