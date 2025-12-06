using Microsoft.Playwright;

namespace Chirp.Web.Tests.PlaywrightUITests;

using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlaywrightEndToEndTest : PageTest
{
    private const string HomePage = "https://bdsa2024group8chirprazor2025.azurewebsites.net/";
    
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
        await Page.GetByPlaceholder("name@example.com").FillAsync("robert@test2.dk");
        await Page.GetByPlaceholder("password").FillAsync("Robert@test.dk2");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();

    }

    private async Task PostCheep(string cheep)
    {
        await Page.GotoAsync(HomePage);
        Console.WriteLine(await Page.ContentAsync());
        await Page.Locator("#CheepText").FillAsync(cheep);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Share" }).ClickAsync();
    }
    [Test]
    public async Task RegisterNewUser_AllowsCheeping()
    {
        await Register();
        await Login();
        await PostCheep("Hi I'm a newly registered user");
        
        Assert.That(await Page.ContentAsync(), Does.Contain("Hi I'm a newly registered user"));
    }
}