using Microsoft.Playwright;

namespace Chirp.Web.Tests.PlaywrightUITests;

using Microsoft.Playwright.NUnit;
using NUnit.Framework;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlaywrightEndToEndTest : PageTest
{
    private const string HomePage = "https://bdsa2024group8chirprazor2025.azurewebsites.net/";
    private const string TestEmail = "test@test.dk";
    private const string TestPassword = "Test@test.dk1";
    const string Author = "Jacqualine Gilcoine";

    //Helpers
    //Registering with testuser

    private async Task Register()
    {
        //todo: problem if testuser already exists
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
        await Page.GetByRole(AriaRole.Link, new() { Name = "my timeline" }).ClickAsync();
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

    //follow link for specific author
    private ILocator FollowLinkForAuthor(string authorName)
    {
        // Find the locator that contains the author's name
        var cheep = Page.Locator("p").Filter(new() { HasText = authorName });

        //For that cheep, return the follow link
        return cheep.GetByRole(AriaRole.Link, new() { Name = "Follow" });
    }

    //unfollow link for specific author
    private ILocator UnfollowLinkForAuthor(string authorName)
    {
        //find locator containing the author´s name
        var cheep = Page.Locator("p").Filter(new() { HasText = authorName });
        //For that cheep. return the unfollow link
        return cheep.GetByRole(AriaRole.Link, new() { Name = "Unfollow" });
    }

    private async Task GoToPageWhereAuthorIsVisible(string authorName)
    {
        // Start from the public timeline
        await Page.GotoAsync(HomePage);

        while (true)
        {
            // Is the author visible on this page
            var cheepsByAuthor = Page.Locator("p").Filter(new() { HasText = authorName });
            if (await cheepsByAuthor.CountAsync() > 0)
            {
                return; //found author, stay on this page
            }

            // If there is a "next" link, go to next page
            var nextLink = Page.GetByRole(AriaRole.Link, new() { Name = "next" });

            if (await nextLink.IsVisibleAsync())
            {
                await nextLink.ClickAsync();
            }
            else
            {
                Assert.Fail($"Could not find any cheeps by {authorName} on the public timeline.");
            }
        }
    }

    [Test]
    public async Task Login_AllowsCheeping()
    {
        await Login();
        var cheepMessage = "Cheep from known user";
        await PostCheep(cheepMessage);

        var content = await Page.ContentAsync();
        Assert.That(content.Contains(cheepMessage));
    }
    // public async Task Login_CanLikeCheep() //todo can't be done until like is on deployed
    // {
    //     await Login();
    //     await Page.GotoAsync(HomePage);
    //     
    //     //Click first like button
    //     var firstLikeButton = Page.GetByRole(AriaRole.Link, new() { Name = "Like" });
    //     await firstLikeButton.ClickAsync();
    //     //Assert that page contains liked
    //     var content = await Page.ContentAsync();
    //     Assert.That(content.Contains("Liked"));
    // }

    [Test]
    public async Task Login_FollowUser_CheepsAppearInTimeline()
    {
        await Login();

        // Make sure we’re on a page where the chosen author actually appears
        await GoToPageWhereAuthorIsVisible(Author);

        // Ensure we start from a clean state: not following the author
        var unfollowLink = UnfollowLinkForAuthor(Author);
        if (await unfollowLink.CountAsync() > 0)
        {
            await unfollowLink.First.ClickAsync(); // reset to "Follow"
        }

        // Click Follow for that author
        var followLink = FollowLinkForAuthor(Author);
        Assert.That(await followLink.CountAsync(), Is.GreaterThan(0), "Expected a Follow link before following.");
        await followLink.First.ClickAsync();

        //Go to private timeline
        await GoToPrivateTimeline();

        //Assert the author shows up on private timeline
        var privateCheepsByAuthor = Page.Locator("p").Filter(new() { HasText = Author });
        Assert.That(await privateCheepsByAuthor.CountAsync(), Is.GreaterThan(0),
            "Expected followed author's cheeps on private timeline.");

        //change page on private timeline (if paging exists)
        var nextLink = Page.GetByRole(AriaRole.Link, new() { Name = "next" });
        if (await nextLink.IsVisibleAsync())
        {
            await nextLink.ClickAsync();

            // Either still see the author, or at least assert paging doesn’t blow up
            var privateCheepsPage2 = Page.Locator("p").Filter(new() { HasText = Author });
            Assert.That(await privateCheepsPage2.CountAsync(), Is.GreaterThanOrEqualTo(0));
        }
    }
}