namespace Chirp.Web.Tests.PlaywrightUITests;

using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;

[TestFixture]
public class PlaywrightFollowTest : PageTest
{
    //url
    private const string HomePage = "https://bdsa2024group8chirprazor2025.azurewebsites.net/";
    const string author = "Jacqualine Gilcoine";
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

    private ILocator FollowLinkForAuthor(string authorName)
    {
        // Find the cheep paragraph that contains the author's name
        var cheep = Page.Locator("p").Filter(new() { HasText = authorName });

        // Inside that cheep, grab the "Follow" link
        return cheep.GetByRole(AriaRole.Link, new() { Name = "Follow" });
    }

    private ILocator UnfollowLinkForAuthor(string authorName)
    {
        var cheep = Page.Locator("p").Filter(new() { HasText = authorName });
        return cheep.GetByRole(AriaRole.Link, new() { Name = "Unfollow" });
    }

    [Test]
    public async Task FollowButtonAppearsOnPublicTimeline()
    {
        await LoginHelperTestUser();
        var followLink = FollowLinkForAuthor(author);
        // Just check we have at least one follow link for her
        var count = await followLink.CountAsync();
        Assert.That(count, Is.GreaterThan(0), "Expected at least one follow link for Jacqualine.");
    }

    [Test]
    public async Task FollowButtonDisappearsOnceFollowed()
    {
            await LoginHelperTestUser();

            //stort by not following Jacqualine
             var unfollowLink = UnfollowLinkForAuthor(author);
             if (await unfollowLink.CountAsync() > 0)
             {
                await unfollowLink.First.ClickAsync(); // reset to follow
             }

            //make sure we can follow her
            var followLinks = FollowLinkForAuthor(author);
            Assert.That(await followLinks.CountAsync(), Is.GreaterThan(0));
            await followLinks.First.ClickAsync(); //follow

            //check that it is possible to unfollow now
            var newUnfollowLinks = UnfollowLinkForAuthor(author);
            Assert.That(await newUnfollowLinks.CountAsync(), Is.GreaterThan(0));
        }

    [Test]
    public async Task UnfollowChangesBackToFollow()
    {
        await LoginHelperTestUser();

         //stort by following Jacqualine
          var followLink = FollowLinkForAuthor(author);
          if (await followLink.CountAsync() > 0)
          {
             await followLink.First.ClickAsync(); // reset to unfollow
          }

         //make sure we can unfollow her
         var unfollowLinks = UnfollowLinkForAuthor(author);
         Assert.That(await unfollowLinks.CountAsync(), Is.GreaterThan(0));
         await unfollowLinks.First.ClickAsync(); //unfollow

         //check that it is possible to follow now
         var newFollowLinks = FollowLinkForAuthor(author);
         Assert.That(await newFollowLinks.CountAsync(), Is.GreaterThan(0));
    }
}