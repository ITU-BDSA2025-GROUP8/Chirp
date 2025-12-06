using Microsoft.Playwright;

namespace Chirp.Web.Tests.PlaywrightUITests;

using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlaywrightEndToEndTest : PageTest
{
    private const string HomePage     = "https://bdsa2024group8chirprazor2025.azurewebsites.net/";
    private const string TestEmail    = "test@test.dk";
    private const string TestPassword = "Test@test.dk1";
    
    //Helpers
    //Registering with testuser
    
    private async Task Register()
    { //todo: problem if testuser already exists
        await Page.GotoAsync(HomePage);
        await Page.GetByRole(AriaRole.Link, new() { Name = "register" }).ClickAsync();
        await Page.GetByPlaceholder("Username").FillAsync("Robert2");
        await Page.GetByPlaceholder("name@example.com").FillAsync("robert@test2.dk");
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("Robert@test.dk2");
        await Page.GetByLabel("Confirm Password").FillAsync("Robert@test.dk2");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();

    }
//log in with testuser
    private async Task Login()
    {
        await Page.GotoAsync(HomePage);
        await Page.GetByRole(AriaRole.Link, new() { Name = "login" }).ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync(TestEmail);
        await Page.GetByPlaceholder("password").FillAsync(TestPassword);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();

    }

    private async Task PostCheep(string cheep)
    {
        await Page.GotoAsync(HomePage);
        await Page.Locator("#CheepText").FillAsync(cheep);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Share" }).ClickAsync();
    }

    private async Task GoToPrivateTimeline()
    {
        await Page.GetByRole(AriaRole.Link, new() { Name = "privat timeline" }).ClickAsync();
    }
    //todo: fix registration
    //[Test]
    // public async Task RegisterNewUser_AllowsCheeping()
    // {
    //     await Register();
    //     await Login();
    //     await PostCheep("Hi I'm a newly registered user");
    //     
    //     Assert.That(await Page.ContentAsync(), Does.Contain("Hi I'm a newly registered user"));
    // }
    [Test]
    public async Task Login_AllowsCheeping()
    {
        await Login();
        var cheepMessage = "Cheep from known user";
        await PostCheep(cheepMessage);
        
        var content = await Page.ContentAsync();
        Assert.That(content.Contains(cheepMessage));
    }
}