using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;

namespace Chirp.Web.Tests.PlaywrightUITests;

[TestFixture]

public class PlaywrightAboutMeTest : PageTest
{
    private string? _username;
    private string? _email;
    private const string Password = "WeAreTesting1";
    private const string HomePage = "https://bdsa2024group8chirprazor2025.azurewebsites.net/";
    
    [SetUp]
    public async Task RegisterUser()
    {
        //Create unique username and email
        var unique = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        _username = $"tester{unique}";
        _email = $"tester{unique}@itu.dk";
        
        await Page.GotoAsync(HomePage);
        
        //Register a new user
        await Page.GetByRole(AriaRole.Link, new() { Name = "register" }).ClickAsync();
        await Page.GetByPlaceholder("username").FillAsync(_username);
        await Page.GetByPlaceholder("name@example.com").FillAsync(_email);
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync(Password);
        await Page.GetByLabel("Confirm Password").FillAsync(Password);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
        
        //Go to about me page
        await Page.GetByRole(AriaRole.Link, new() { Name = "About me" }).ClickAsync();
    }

    [Test]
    public async Task ShowAboutMePageCorrectly() 
    {
        //Check that the page is rendered correctly
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "About Me" }))
            .ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = "Feature", Exact = true }))
            .ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = "Information", Exact = true }))
            .ToBeVisibleAsync();
        
        
        //Check that the information is correct
        //Username
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = "Name", Exact = true }))
            .ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = _username, Exact = true }))
            .ToBeVisibleAsync();
        
        //Email
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = "Email", Exact = true }))
            .ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Cell, new() { Name = _email, Exact = true }))
            .ToBeVisibleAsync();

        //Following
        var following = Page
            .GetByRole(AriaRole.Cell, new() { Name = "Following", Exact = true })
            .Locator("xpath=following-sibling::td");

        await Expect(following).ToBeVisibleAsync();
        await Expect(following).ToHaveTextAsync("You are not following anyone.");
        
        //Cheeps
        var cheeps = Page
            .GetByRole(AriaRole.Cell, new() { Name = "Cheeps", Exact = true })
            .Locator("xpath=following-sibling::td");
        await Expect(cheeps).ToBeVisibleAsync();
        await Expect(cheeps).ToHaveTextAsync("There are no cheeps so far.");
    }

    [TearDown]
    public async Task DeleteUser()
    {
        //Delete user
        await Page.GetByRole(AriaRole.Link, new() { Name = "About me" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Forget me!" }).ClickAsync();
    }
}

