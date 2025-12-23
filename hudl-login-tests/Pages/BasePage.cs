using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace hudl_login_tests.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly WebDriverWait Wait;

        protected BasePage(IWebDriver driver, int timeoutSeconds = 10)
        {
            Driver = driver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
        }

        protected bool IsElementVisible(By locator)
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return Driver.FindElement(locator).Displayed;
            }
            catch { return false; }
        }

        protected string GetElementText(By locator)
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return Driver.FindElement(locator).Text;
            }
            catch { return string.Empty; }
        }

        protected string GetElementAttribute(By locator, string attributeName)
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementIsVisible(locator));
                return Driver.FindElement(locator).GetAttribute(attributeName) ?? string.Empty;
            }
            catch { return string.Empty; }
        }

        protected bool WaitForUrlContains(string urlPattern)
        {
            try
            {
                Wait.Until(d => d.Url.Contains(urlPattern));
                return Driver.Url.Contains(urlPattern);
            }
            catch { return false; }
        }

        protected bool WaitForUrlStartsWith(string urlPattern)
        {
            try
            {
                Wait.Until(d => d.Url.StartsWith(urlPattern));
                return Driver.Url.StartsWith(urlPattern);
            }
            catch { return false; }
        }

        protected void ClickElement(By locator)
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            Driver.FindElement(locator).Click();
        }

        protected void ClickWithJavaScript(By locator)
        {
            Wait.Until(ExpectedConditions.ElementToBeClickable(locator));
            var element = Driver.FindElement(locator);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", element);
        }

        protected void EnterText(By locator, string text)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            var element = Driver.FindElement(locator);
            element.Clear();
            element.SendKeys(text);
        }

        protected void HoverOverElement(By locator)
        {
            Wait.Until(ExpectedConditions.ElementIsVisible(locator));
            var element = Driver.FindElement(locator);
            var actions = new Actions(Driver);
            actions.MoveToElement(element).Perform();
        }

        protected bool HasElements(By locator)
        {
            return Driver.FindElements(locator).Count > 0;
        }

        protected IReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            return Driver.FindElements(locator);
        }
    }
}
