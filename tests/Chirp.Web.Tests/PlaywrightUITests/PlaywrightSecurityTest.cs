using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Chirp.Web.Tests.PlaywrightUITests;

[TestFixture]
public class PlaywrightSecurityTest: PageTest
{
    [Test] 
    public async Task CheepboxCanSustainXSSandSQLAttacks() 
    {
        //Login as user registered with SQL injection as username
        await Page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/");
        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync("robert@test.dk");
        await Page.GetByPlaceholder("password").FillAsync("Robert@test.dk1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Public timeline" }).ClickAsync();
        await Expect(Page.GetByText("Public Timeline What's on")).ToBeVisibleAsync();
        
        //Test with XSS attack
        await Page.Locator("#CheepText").ClickAsync();
        await Page.Locator("#CheepText").FillAsync("Hello, I am feeling good!<script>alert('If you see this in a popup, you are in trouble!');</script>"); //input text is directly from slides from lecture 10
        await Page.GetByRole(AriaRole.Button, new() { Name = "Share" }).ClickAsync();
        await Expect(Page.GetByText("Robert'); DROP TABLE Cheeps;-- Hello, I am  feeling good!<script>alert('If you see this in a popup, you are in trouble!');</script>").First).ToBeVisibleAsync();
    }
}