using Microsoft.Playwright;

namespace Chirp.Web.Tests.PlaywrightUITests;

using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlaywrightUITest : PageTest
{
    [Test]
    public async Task CheepBoxAppearsWhenUserHasLoggedIn()
    {
        //visit homepage not logged in
        await Page.GotoAsync("https://localhost:5273");
        //cheepbox should not be visible
        var cheepBox = Page.Locator(".CheepBox");
        await Expect(cheepBox).Not.ToBeVisibleAsync();
        //log in
        await Page.GetByRole(AriaRole.Link, new() { Name = "Log in" }).ClickAsync();
        await Page.GetByLabel("User Name").FillAsync("testuser");
        await Page.GetByLabel("Password").FillAsync("Pa$$word123");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        //go back to homepage after login
        await Page.GotoAsync("https://localhost:5273");
        //CheepBox should be visible
        cheepBox = Page.Locator(".CheepBox");
        await Expect(cheepBox).ToBeVisibleAsync();
    }
}