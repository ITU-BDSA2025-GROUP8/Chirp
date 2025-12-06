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
        await Page.GetByPlaceholder("Name").FillAsync("Robert2");
        await Page.GetByPlaceholder("Email").FillAsync("robert@test2.dk");
        await Page.GetByPlaceholder("Password").FillAsync("Robert@test2.dk1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
        
    }
//log in with testuser
    private async Task Login()
    {
        await Page.GotoAsync(HomePage);
        await Page.GetByRole(AriaRole.Link, new() { Name = "login" }).ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync("robert@test2.dk");
        await Page.GetByPlaceholder("password").FillAsync("Robert@test2.dk");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();

    }
    [Test]
    public async Task cheepInDatabase()
    {

    }
}