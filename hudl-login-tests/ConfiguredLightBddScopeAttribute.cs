using LightBDD.Core.Configuration;
using LightBDD.Extensions.DependencyInjection;
using LightBDD.Framework.Reporting;
using LightBDD.Framework.Reporting.Formatters;
using LightBDD.XUnit2;

[assembly: hudl_login_tests.ConfiguredLightBddScope]

namespace hudl_login_tests
{
    public class ConfiguredLightBddScopeAttribute : LightBddScopeAttribute
    {
        protected override void OnConfigure(LightBddConfiguration configuration)
        {
            configuration.StepTypeConfiguration()
                .UpdatePredefinedStepTypes("given", "when", "then", "but")
                .UpdateRepeatedStepReplacement("and also");
            configuration
                .DependencyContainerConfiguration()
                .UseContainer(DependencyInjection.BuildServiceProvider(), takeOwnership: false);

            // Go up from bin/Debug/net8.0 to project root, then into TestResults/Reports
            var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
            var reportPath = Path.Combine(projectRoot, "TestResults", "Reports", "FeaturesReport.html");

            configuration
                .ReportWritersConfiguration()
                .Add(new ReportFileWriter(new HtmlReportFormatter(), reportPath));
        }
    }
}
