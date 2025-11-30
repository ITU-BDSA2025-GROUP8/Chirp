namespace Chirp.Web.Tests.PlaywrightUITests;

using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlaywrightFollowTest : PageTest
{
    //url
    private const string HomePage = "https://bdsa2024group8chirprazor2025.azurewebsites.net/";
    //loginHelper
    private async Task LoginHelperTestUser()
    {
        //log in
        await Page.GotoAsync(HomePage);
        await Page.GetByRole(AriaRole.Link, new() { Name = "login" }).ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync("robert@test.dk");
        await Page.GetByPlaceholder("password").FillAsync("Robert@test.dk1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();        
        //go back to homepage after login
        await Page.GotoAsync(HomePage);
    }
    [Test]
    public async Task FollowButtonAppearsOnPublicTimeline()
    {
        await LoginHelperTestUser();
        //Follow button should be visible
        // cheepBox = Page.Locator(".CheepBox");
        // await Expect(cheepBox).ToBeVisibleAsync();
    }

    [Test]
    public async Task FollowButtonDisappearsOnceFollowed()
    {
        
    }
    [Test]
    public async Task FollowButtonDisappearsOnceFollowed()
    {
        
    }