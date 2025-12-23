using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Chirp.Web.Tests.PlaywrightUITests;

[TestFixture]
public class PlaywrightForgetMeTest : PageTest
{
    [Test]
    public async Task ForgetMeTest()
    {
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net");
        
        // Register
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Register" }).ClickAsync();
        await Page.GetByPlaceholder("Username").FillAsync("TestOfForgetMe");
        await Page.GetByPlaceholder("name@example.com").FillAsync("TestOfForgetMe@test.dk");
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("TestOfForgetMe@test.dk1");
        await Page.GetByLabel("Confirm Password", new() { Exact = true }).FillAsync("TestOfForgetMe@test.dk1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
        
        // Make cheep
        await Page.Locator("#CheepText").ClickAsync();
        await Page.Locator("#CheepText").FillAsync("Test that this is deleted when i press the 'Forget Me!' button");
        await Page.Locator("#CheepText").PressAsync("Enter");
        
        // Forget me
        await Page.GetByRole(AriaRole.Link, new() { Name = "About me" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Forget me!" }).ClickAsync();
        
        // Assert user have been logged out and cheep is also deleted
        await Expect(Page.Locator("#messagelist")).Not
            .ToContainTextAsync("Test that this is deleted when i press the 'Forget Me!' button");
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "login" })).ToBeVisibleAsync();
        
        // Assert user can't log in again
        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync("robert@test.dk");
        await Page.GetByPlaceholder("password").FillAsync("Robert@test.dk1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        
        // Can register with same credentials
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Register" }).ClickAsync();
        await Page.GetByPlaceholder("Username").FillAsync("TestOfForgetMe");
        await Page.GetByPlaceholder("name@example.com").FillAsync("TestOfForgetMe@test.dk");
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("TestOfForgetMe@test.dk1");
        await Page.GetByLabel("Confirm Password", new() { Exact = true }).FillAsync("TestOfForgetMe@test.dk1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();

        // Assert cheep is also deleted
        await Page.GetByRole(AriaRole.Link, new() { Name = "My timeline" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Emphasis)).ToContainTextAsync("There are no cheeps so far.");

        // Forget me - to clean up
        await Page.GetByRole(AriaRole.Link, new() { Name = "About me" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Forget me!" }).ClickAsync();
    }
}

