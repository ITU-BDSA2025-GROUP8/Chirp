namespace Chirp.Web.Tests.PlaywrightUITests;

using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;

[TestFixture]
public class PlaywrightFollowTest : PageTest
{
    // Url
    private const string HomePage = "https://bdsa2024group8chirprazor2025.azurewebsites.net/";

    const string Author = "Jacqualine Gilcoine";

    // LoginHelper
    // Logs in Robert and ensures return to public timeline
    private async Task LoginHelperTestUser()
    {
        // Log in
        await Page.GotoAsync(HomePage);
        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync("robert@test.dk");
        await Page.GetByPlaceholder("password").FillAsync("Robert@test.dk1");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        // Redirect to homepage after login
        await Page.GotoAsync(HomePage);
    }

    // Follow link for specific author
    private ILocator FollowLinkForAuthor(string authorName)
    {
        // Find the locator that contains the author's name
        var cheep = Page.Locator("p").Filter(new() { HasText = authorName });

        // For that cheep, return the follow link
        return cheep.GetByRole(AriaRole.Link, new() { Name = "Follow" });
    }

    // Unfollow link for specific author
    private ILocator UnfollowLinkForAuthor(string authorName)
    {
        // Find locator containing the author´s name
        var cheep = Page.Locator("p").Filter(new() { HasText = authorName });
        // For that cheep. return the unfollow link
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
                return; // Found author, stay on this page
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

        // Start by not following Jacqualine
        var unfollowLink = UnfollowLinkForAuthor(Author);
        if (await unfollowLink.CountAsync() > 0)
        {
            await unfollowLink.First.ClickAsync(); // reset to follow
        }

        // Click follow
        var followLink = FollowLinkForAuthor(Author);
        Assert.That(await followLink.CountAsync(), Is.GreaterThan(0));
        await followLink.First.ClickAsync(); //follow

        // Expect unfollow
        var newUnfollowLinks = UnfollowLinkForAuthor(Author);
        Assert.That(await newUnfollowLinks.CountAsync(), Is.GreaterThan(0));
    }

    [Test]
    public async Task UnfollowChangesBackToFollow()
    {
        await LoginHelperTestUser();
        await GoToPageWhereAuthorIsVisible(Author);

        // Start by following Jacqualine
        var followLink = FollowLinkForAuthor(Author);
        if (await followLink.CountAsync() > 0)
        {
            await followLink.First.ClickAsync();
        }
        // Click unfollow
        var unfollowLink = UnfollowLinkForAuthor(Author);
        await unfollowLink.First.ClickAsync();

        // Expect follow
        var newFollowLinks = FollowLinkForAuthor(Author);
        Assert.That(await newFollowLinks.CountAsync(), Is.GreaterThan(0));
    }
}