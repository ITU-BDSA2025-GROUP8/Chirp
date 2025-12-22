using Microsoft.AspNetCore.SignalR;
using Microsoft.Playwright;
using Chirp.Web.Tests.PlaywrightUITests;

namespace Chirp.Web.Tests.PlaywrightUITests;

using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlaywrightEndToEndTest : PageTest
{
    private const string HomePage = "https://bdsa2024group8chirprazor2025.azurewebsites.net/";
    private const string TestEmail = "robert@test.dk";
    private const string TestPassword = "Robert@test.dk1";

    //Helpers
    //Registering with test user

    private async Task Register()
    {
        //Generate unique suffix
        var unique = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        
        var uniqueUsername = $"Test{unique}";
        var uniqueEmail = $"{uniqueUsername}@test.dk";
        
        await Page.GotoAsync(HomePage);
        await Page.GetByRole(AriaRole.Link, new() { Name = "register" }).ClickAsync();
        await Page.GetByPlaceholder("Username").FillAsync(uniqueUsername);
        await Page.GetByPlaceholder("name@example.com").FillAsync(uniqueEmail);
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("Robert@test.dk2");
        await Page.GetByLabel("Confirm Password").FillAsync(TestPassword);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();

    }

//log in with test user
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
    
    
    [Test]
     public async Task RegisterNewUser_AllowsCheeping()
     {
         await Register();
         await Login();
         await PostCheep("Hi I'm a newly registered user");
         
         Assert.That(await Page.ContentAsync(), Does.Contain("Hi I'm a newly registered user"));
     }

    //follow link for specific author

    [Test]
    public async Task Login_AllowsCheeping()
    {
        await Login();
        var cheepMessage = "Cheep from known user";
        await PostCheep(cheepMessage);

        var content = await Page.ContentAsync();
        Assert.That(content.Contains(cheepMessage));
    }
     public async Task Login_CanLikeCheep() 
     {
         await Login();
         await Page.GotoAsync(HomePage);
         
         //Click first like button
         var firstLikeButton = Page.GetByRole(AriaRole.Link, new() { Name = "Like" });
         await firstLikeButton.ClickAsync();
         //Assert that page contains liked
         var content = await Page.ContentAsync();
         Assert.That(content.Contains("Liked"));
     }
}