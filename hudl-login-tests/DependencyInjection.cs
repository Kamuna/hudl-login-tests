using hudl_login_tests.Features;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;

namespace hudl_login_tests
{
    public static class DependencyInjection
    {

        public static ServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            services
                .AddWebDriver()
                .AddSteps();
            return services.BuildServiceProvider();
        }

        public static ServiceCollection AddWebDriver(this ServiceCollection services)
        {
            services.AddScoped<IWebDriver>(_ => WebDriverFactory.Create());
            return services;
        }

        public static ServiceCollection AddSteps(this ServiceCollection services)
        {
            services.AddScoped<HudlLoginSteps>();
            return services;
        }
    }
}
