
using hudl_login_tests.Configuration;
using hudl_login_tests.Utils;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using Xunit;

namespace hudl_login_tests.Features
{
    [FeatureDescription(
@"In order to access my Hudl account and personal data
As a Hudl user
I want to be able to login securely into the system")]
    [Label("HUDL-Login")]
    public class HudlLoginShould : FeatureFixture
    {
        [Scenario]
        [ScenarioCategory("Authentication")]
        public async Task SuccessfullyLoginAUser_GivenValidCredentials()
        {
            await Runner
                .WithContext<HudlLoginSteps>()
                .AddAsyncSteps(
                    given => given.AUserNavigatesToTheLoginPage(),
                    when => when.UserLoginsInWithValidCredentials(),
                    then => then.UserShouldBeLoggedInSuccessfully()
                )
                .RunAsync();
        }

        [Scenario]
        [ScenarioCategory("Authentication")]
        public async Task DisplayErrorMessage_GivenInvalidPassword()
        {
            await Runner
                .WithContext<HudlLoginSteps>()
                .AddAsyncSteps(
                    given => given.AUserNavigatesToTheLoginPage(),
                    when => when.UserLoginsInWithInvalidPassword(),
                    then => then.AnInvalidPasswordErrorMessageShouldBeDisplayed()
                )
                .RunAsync();
        }

        [Scenario]
        [ScenarioCategory("Validation")]
        [MemberData(nameof(TestDataProvider.InvalidEmailTestData), MemberType = typeof(TestDataProvider))]
        public async Task DisplayValidationError_GivenInvalidEmailInput(string email, string expectedError)
        {
            await Runner
                .WithContext<HudlLoginSteps>()
                .AddAsyncSteps(
                    given => given.AUserNavigatesToTheLoginPage(),
                    when => when.UserEntersEmailAndClicksContinue(email),
                    then => then.EmailErrorMessageShouldBeDisplayed(expectedError)
                )
                .RunAsync();
        }

        [Scenario]
        [ScenarioCategory("Validation")]
        [MemberData(nameof(TestDataProvider.BoundaryTestData), MemberType = typeof(TestDataProvider))]
        public async Task HandleInput_GivenExcessivelyLongValue(string field, int length)
        {
            await Runner
                .WithContext<HudlLoginSteps>()
                .AddAsyncSteps(
                    given => given.AUserNavigatesToTheLoginPage(),
                    when => when.UserEntersExcessivelyLongInput(field, length),
                    then => then.ApplicationShouldHandleLongInputGracefully()
                )
                .RunAsync();
        }

        [Scenario]
        [ScenarioCategory("Validation")]
        public async Task DisplayValidationError_GivenEmptyPassword()
        {
            await Runner
                .WithContext<HudlLoginSteps>()
                .AddAsyncSteps(
                    given => given.AUserNavigatesToTheLoginPage(),
                    when => when.UserSubmitsEmptyPassword(),
                    then => then.EmptyPasswordErrorMessageShouldBeDisplayed()
                )
                .RunAsync();
        }

        [Scenario]
        [ScenarioCategory("UI")]
        [ScenarioCategory("Accessibility")]
        public async Task TogglePasswordVisibility_WhenClickingEyeIcon()
        {
            await Runner
                .WithContext<HudlLoginSteps>()
                .AddAsyncSteps(
                    given => given.AUserNavigatesToTheLoginPage(),
                    and => and.UserEntersEmailAndProceedsToPasswordScreen(),
                    and => and.UserEntersPasswordOnScreen(),
                    and => and.PasswordFieldShouldBeMasked(),
                    when => when.UserClicksPasswordVisibilityToggle(),
                    then => then.PasswordFieldShouldBeVisible(),
                    when => when.UserClicksPasswordVisibilityToggle(),
                    then => then.PasswordFieldShouldBeMasked()
                )
                .RunAsync();
        }

        [Scenario]
        [ScenarioCategory("Authentication")]
        public async Task SuccessfullyLogout_AfterLogin()
        {
            await Runner
                .WithContext<HudlLoginSteps>()
                .AddAsyncSteps(
                    given => given.AUserNavigatesToTheLoginPage(),
                    and => and.UserLoginsInWithValidCredentials(),
                    and => and.UserShouldBeLoggedInSuccessfully(),
                    when => when.UserClicksLogout(),
                    then => then.UserShouldBeLoggedOut()
                )
                .RunAsync();
        }

        [Scenario]
        [ScenarioCategory("Security")]
        public async Task NotAllowAccess_WhenPressingBackButtonAfterLogout()
        {
            await Runner
                .WithContext<HudlLoginSteps>()
                .AddAsyncSteps(
                    given => given.AUserNavigatesToTheLoginPage(),
                    and => and.UserLoginsInWithValidCredentials(),
                    and => and.UserShouldBeLoggedInSuccessfully(),
                    and => and.UserClicksLogout(),
                    and => and.UserShouldBeLoggedOut(),
                    when => when.UserPressesBackButton(),
                    and => and.UserHoversOverProfileAndClicksAccountSettings(),
                    then => then.UserShouldBeRedirectedToLoginPage()
                )
                .RunAsync();
        }

        [Scenario]
        [ScenarioCategory("Authentication")]
        [MemberData(nameof(TestDataProvider.EmailCaseSensitivityTestData), MemberType = typeof(TestDataProvider))]
        public async Task HandleLogin_GivenDifferentEmailCasing(string email)
        {
            await Runner
                .WithContext<HudlLoginSteps>()
                .AddAsyncSteps(
                    given => given.AUserNavigatesToTheLoginPage(),
                    when => when.UserLoginsWithEmail(email),
                    then => then.UserShouldBeLoggedInSuccessfully()
                )
                .RunAsync();
        }

        [Scenario]
        [ScenarioCategory("UI")]
        [ScenarioCategory("Navigation")]
        public async Task AllowUserToEditEmail_AfterClickingContinue()
        {
            await Runner
                .WithContext<HudlLoginSteps>()
                .AddAsyncSteps(
                    given => given.AUserNavigatesToTheLoginPage(),
                    and => and.UserEntersEmail(TestConfiguration.WrongEmail),
                    and => and.UserClicksContinue(),
                    when => when.UserClicksEditEmailLink(),
                    then => then.UserShouldBeOnEmailEntryScreen(),
                    and => and.UserEntersEmail(TestConfiguration.ValidUserEmail),
                    and => and.UserClicksContinue(),
                    and => and.UserEntersPassword(TestConfiguration.ValidUserPassword),
                    and => and.UserClicksSubmit(),
                    then => then.UserShouldBeLoggedInSuccessfully()
                )
                .RunAsync();
        }

        [Scenario]
        [ScenarioCategory("Authentication")]
        [ScenarioCategory("PasswordReset")]
        public async Task InitiatePasswordReset_GivenValidEmail()
        {
            await Runner
                .WithContext<HudlLoginSteps>()
                .AddAsyncSteps(
                    given => given.AUserNavigatesToTheLoginPage(),
                    and => and.UserEntersEmail(TestConfiguration.ValidUserEmail),
                    and => and.UserClicksContinue(),
                    when => when.UserClicksForgotPasswordLink(),
                    then => then.UserShouldBeOnResetPasswordPage(),
                    and => and.EmailFieldShouldBePrePopulatedWith(TestConfiguration.ValidUserEmail),
                    when => when.UserClicksResetPasswordContinue(),
                    then => then.UserShouldSeeCheckYourEmailPage(),
                    and => and.ResendEmailLinkShouldBeVisible()
                )
                .RunAsync();
        }

        [Scenario]
        [ScenarioCategory("Authentication")]
        [ScenarioCategory("PasswordReset")]
        public async Task InitiatePasswordReset_FromAccountSettingsPage()
        {
            await Runner
                .WithContext<HudlLoginSteps>()
                .AddAsyncSteps(
                    given => given.AUserNavigatesToTheLoginPage(),
                    and => and.UserLoginsInWithValidCredentials(),
                    and => and.UserShouldBeLoggedInSuccessfully(),
                    when => when.UserClicksAccountSettings(),
                    then => then.UserShouldBeOnAccountSettingsPage(),
                    when => when.UserClicksResetPasswordButton(),
                    then => then.PasswordResetSuccessToastShouldBeDisplayed()
                )
                .RunAsync();
        }
    }
}
