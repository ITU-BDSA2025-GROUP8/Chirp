using Microsoft.Playwright;

namespace Chirp.Web.Tests.PlaywrightUITests;

using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlaywrightUITest : PageTest
{
    [SetUp]
    public async Task SetUp()
    {
        //enter something here
        //todo: obs. logging in is not possible in setup, as par op "CheepBoxAppears..." is that we have to check if something appears before logging in
        
    }
    
    [Test]
    public async Task CheepBoxAppearsWhenUserHasLoggedIn()
    {
        //visit homepage not logged in
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/");
        //cheepbox should not be visible
        var cheepBox = Page.Locator(".CheepBox");
        await Expect(cheepBox).Not.ToBeVisibleAsync();
        //log in
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Log in" }).ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync("robert@test.dk");
        await Page.GetByPlaceholder("password").FillAsync("Robert@test.dk1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        //go back to homepage after login
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/");
        //CheepBox should be visible
        cheepBox = Page.Locator(".CheepBox");
        await Expect(cheepBox).ToBeVisibleAsync();
    }
    
    [Test]
    public async Task BehaviourWhenCheepIsLongerThan160Chars()
    {
        //visit homepage not logged in
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/");
        //cheepbox should not be visible
        var cheepBox = Page.Locator(".CheepBox");
        await Expect(cheepBox).Not.ToBeVisibleAsync();
        //log in
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Log in" }).ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync("robert@test.dk");
        await Page.GetByPlaceholder("password").FillAsync("Robert@test.dk1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();

    }

    [TearDown]
    public void TearDown()
    {
        //todo: something in here?
    }
}