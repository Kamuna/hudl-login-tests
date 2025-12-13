
using LightBDD.Framework.Scenarios;
using LightBDD.XUnit2;
using Xunit;

namespace hudl_login_tests.Features
{
    public class HudlLoginShould : FeatureFixture
    {

        [Scenario]
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
        [InlineData("", "empty email")]
        [InlineData("invalid-email-format", "invalid email format")]
        [InlineData("notarealemail", "invalid email")]
        public async Task DisplayValidationError_GivenInvalidEmailInput(string email, string scenario)
        {
            await Runner
                .WithContext<HudlLoginSteps>()
                .AddAsyncSteps(
                    given => given.AUserNavigatesToTheLoginPage(),
                    when => when.UserEntersEmailAndClicksContinue(email),
                    then => then.AnInvalidEmailErrorMessageShouldBeDisplayed()
                )
                .RunAsync();
        }

        [Scenario]
        public async Task DisplayValidationError_GivenEmptyPassword()
        {
            await Runner
                .WithContext<HudlLoginSteps>()
                .AddAsyncSteps(
                    given => given.AUserNavigatesToTheLoginPage(),
                    when => when.UserSubmitsEmptyPassword(),
                    then => then.AValidationErrorShouldBeDisplayed()
                )
                .RunAsync();
        }

        [Scenario]
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

    }
}
