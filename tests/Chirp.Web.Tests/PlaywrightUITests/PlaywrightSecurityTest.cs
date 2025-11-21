using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace Chirp.Web.Tests.PlaywrightUITests;

[TestFixture]
public class PlaywrightSecurityTest: PageTest
{
    [Test] 
    public async Task CheepboxCanSustainXSSAtacks() 
    {
        //todo: the test fails as it should be run when a user is logged in, so there is a cheep-box in the page
        await Page.Locator("#CheepText").ClickAsync();
        await Page.Locator("#CheepText").FillAsync("Hello, I am feeling good!<script>alert('If you see this in a popup, you are in trouble!');</script>"); //input text is directly from slides from lecture 10
        await Page.GetByRole(AriaRole.Button, new() { Name = "Share" }).ClickAsync();
        await Expect(Page.GetByText("TestyTester Hello, I am").First).ToBeVisibleAsync();
        await Expect(Page.Locator("#messagelist")).ToContainTextAsync("TestyTester Hello, I am feeling good!<script>alert('If you see this in a popup, you are in trouble!');</script> — 21. november 2025");
    }
}