using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace hudl_login_tests.Features.StepDefinitions
{
   
    public class SecuritySteps : BaseSteps
    {
        public SecuritySteps(IWebDriver driver) : base(driver) { }

       
        public async Task UserPressesBackButton()
        {
            await ExecuteStep(nameof(UserPressesBackButton), () =>
            {
                Driver.Navigate().Back();
            });
        }

       
        public async Task UserHoversOverProfileAndClicksAccountSettings()
        {
            await ExecuteStep(nameof(UserHoversOverProfileAndClicksAccountSettings), () =>
            {
                Dashboard.HoverOverProfileMenu();
                Dashboard.ClickAccountSettings();
            });
        }

        
        public async Task UserShouldBeRedirectedToLoginPage()
        {
            await ExecuteStep(nameof(UserShouldBeRedirectedToLoginPage), () =>
            {
                var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.Url.Contains("identity.hudl.com") || d.Url.Contains("login"));

                var currentUrl = Driver.Url;
                AssertTrue(currentUrl.Contains("identity.hudl.com") && currentUrl.Contains("login"),
                    $"User should be redirected to identity login page. Current URL: {currentUrl}");
            });
        }
    }
}
