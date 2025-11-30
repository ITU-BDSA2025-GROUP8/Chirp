namespace Chirp.Web.Tests.PlaywrightUITests;

using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;

[TestFixture]
public class PlaywrightFollowTest : PageTest
{
    //url
    private const string HomePage = "https://bdsa2024group8chirprazor2025.azurewebsites.net/";

    const string Author = "Jacqualine Gilcoine";

    //loginHelper
    //logs in Robert and ensures return to public timeline
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
    public async Task FollowButtonAppearsOnPublicTimeline()
    {
        await LoginHelperTestUser();
        var followLink = FollowLinkForAuthor(Author);
        // Just check we have at least one follow link for her
        var count = await followLink.CountAsync();
        Assert.That(count, Is.GreaterThan(0), "Expected follow link for Jacqualine.");
    }

    [Test]
    public async Task FollowChangesToUnfollow()
    {
        await LoginHelperTestUser();

        //stort by not following Jacqualine
        var unfollowLink = UnfollowLinkForAuthor(Author);
        if (await unfollowLink.CountAsync() > 0)
        {
            await unfollowLink.First.ClickAsync(); // reset to follow
        }

        //Click follow
        var followLink = FollowLinkForAuthor(Author);
        Assert.That(await followLink.CountAsync(), Is.GreaterThan(0));
        await followLink.First.ClickAsync(); //follow

        //expect unfollow
        var newUnfollowLinks = UnfollowLinkForAuthor(Author);
        Assert.That(await newUnfollowLinks.CountAsync(), Is.GreaterThan(0));
    }

    [Test]
    public async Task UnfollowChangesBackToFollow()
    {
        await LoginHelperTestUser();
        await GoToPageWhereAuthorIsVisible(Author);

        //stort by following Jacqualine
        var followLink = FollowLinkForAuthor(Author);
        if (await followLink.CountAsync() > 0)
        {
            await followLink.First.ClickAsync();
        }
        //Click unfollow
        var unfollowLink = UnfollowLinkForAuthor(Author);
        await unfollowLink.First.ClickAsync();

        //Expect follow
        var newFollowLinks = FollowLinkForAuthor(Author);
        Assert.That(await newFollowLinks.CountAsync(), Is.GreaterThan(0));
    }
}