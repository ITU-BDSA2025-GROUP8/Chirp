namespace Chirp.Web.Tests.PlaywrightUITests;

using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;

[TestFixture]

public class PlaywrightAboutMeTest : PageTest
{

    [Test]
    public async Task ShowAboutMePageCorrectly()
    {
        var username = "Tester";
        var email = "Tester@itu.dk";
        var password = "WeAreTesting1";
        
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/");
        
        //Register a new user
        await Page.GetByRole(AriaRole.Link, new() { Name = "register" }).ClickAsync();
        await Page.GetByPlaceholder("Username").FillAsync(username);
        await Page.GetByPlaceholder("name@example.com").FillAsync(email);
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync(password);
        await Page.GetByLabel("Confirm Password").FillAsync(password);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
        
        await Page.WaitForURLAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/");

        
        //Go to about me page
        await Page.GetByRole(AriaRole.Link, new() { Name = "About me" }).ClickAsync();
        
        //Collect information shown
        await Expect(Page.GetByText("About me")).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = "Feature", Exact = true }))
            .ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = "Information", Exact = true }))
            .ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = "Name", Exact = true }))
            .ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = username, Exact = true }))
            .ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = "Email", Exact = true }))
            .ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = email, Exact = true }))
            .ToBeVisibleAsync();

        var following = Page
            .GetByRole(AriaRole.Cell, new() { Name = "Following", Exact = true })
            .Locator("xpath=following-sibling::td");

        await Expect(following).ToBeVisibleAsync();
        await Expect(following).ToHaveTextAsync("You are not following anyone.");
        
        var cheeps = Page
            .GetByRole(AriaRole.Cell, new() { Name = "Cheeps", Exact = true })
            .Locator("xpath=following-sibling::td");

        await Expect(cheeps).ToBeVisibleAsync();
        await Expect(cheeps).ToHaveTextAsync("There are no cheeps so far.");

    }

}

