using OpenQA.Selenium;

namespace hudl_login_tests.Pages
{
    public class HudlAccountSettingsPage : BasePage
    {
        public HudlAccountSettingsPage(IWebDriver driver) : base(driver, 15) { }

        private const string AccountSettingsUrlPattern = "hudl.com/profile";

        private By ResetPasswordButton => By.XPath("//*[@id='resetPassword']");
        private By SuccessToast => By.XPath("//*[@id='SuccessToast']/div[2]");

        public bool IsOnAccountSettingsPage() => WaitForUrlContains(AccountSettingsUrlPattern);

        public void ClickResetPassword() => ClickWithJavaScript(ResetPasswordButton);

        public bool IsSuccessToastDisplayed() => IsElementVisible(SuccessToast);

        public string GetSuccessToastText() => GetElementText(SuccessToast);
    }
}
