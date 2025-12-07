using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Assert = Xunit.Assert;

namespace Chirp.Web.Tests.PlaywrightUITests;

[TestFixture]
public class PlaywrightForgetMeTest : PageTest
{
    [Test]
    public async Task ForgetMeTest()
    {
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net");
        //Register
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net");
        await Page.GetByRole(AriaRole.Link, new() { Name = "register" }).ClickAsync();
        await Page.GetByPlaceholder("Username").FillAsync("TestOfForgetMe");
        await Page.GetByPlaceholder("name@example.com").FillAsync("TestOfForgetMe@test.dk");
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("TestOfForgetMe@test.dk1");
        await Page.GetByLabel("Confirm Password", new() { Exact = true }).FillAsync("TestOfForgetMe@test.dk1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
        //make cheep
        await Page.Locator("#CheepText").ClickAsync();
        await Page.Locator("#CheepText").FillAsync("Test that this is deleted when i press the 'Forget Me!' button");
        await Page.Locator("#CheepText").PressAsync("Enter");
        
        //forget me
        await Page.GetByRole(AriaRole.Link, new() { Name = "About me" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Forget me!" }).ClickAsync();
        
        //assert user have been logged out and cheep is also deleted
        await Expect(Page.Locator("#messagelist")).Not
            .ToContainTextAsync("Test that this is deleted when i press the 'Forget Me!' button");
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "login" })).ToBeVisibleAsync();
        
        //can't log in again
        await Page.GetByRole(AriaRole.Link, new() { Name = "login" }).ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync("robert@test.dk");
        await Page.GetByPlaceholder("password").FillAsync("Robert@test.dk1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        
        //Can register with same credentials
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net");
        await Page.GetByRole(AriaRole.Link, new() { Name = "register" }).ClickAsync();
        await Page.GetByPlaceholder("Username").FillAsync("TestOfForgetMe");
        await Page.GetByPlaceholder("name@example.com").FillAsync("TestOfForgetMe@test.dk");
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("TestOfForgetMe@test.dk1");
        await Page.GetByLabel("Confirm Password", new() { Exact = true }).FillAsync("TestOfForgetMe@test.dk1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();

        //assert cheep is also deleted
        await Page.GetByRole(AriaRole.Link, new() { Name = "My timeline" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Emphasis)).ToContainTextAsync("There are no cheeps so far.");

        //forget me - to clean up
        await Page.GetByRole(AriaRole.Link, new() { Name = "About me" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Forget me!" }).ClickAsync();
    }
}

