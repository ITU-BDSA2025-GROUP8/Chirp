using Microsoft.Playwright;
using System;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using var playwright = await Playwright.CreateAsync();
await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
{
    Headless = false,
});

public class PlaywrightPageButtonTest : PageTest
{
    [Test]
    public async Task NextPreviousButtonsChangesPagesOnPublicTimeline()
    {
        var context = await Browser.NewContextAsync();
        var page = await context.NewPageAsync();
        
        // Check previous button is invisible on page 1
        await page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/");
        var previous = page.GetByRole(AriaRole.Link, new() { Name = "previous" });
        await Expect(previous).Not.ToBeVisibleAsync();
        
        // Check url option is correct after pressing next
        await page.GetByRole(AriaRole.Link, new() { Name = "next" }).ClickAsync();
        await Expect(page).ToHaveURLAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/?page=2");
        
        await page.GetByRole(AriaRole.Link, new() { Name = "next" }).ClickAsync();
        await Expect(page).ToHaveURLAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/?page=3");
        
        await page.GetByRole(AriaRole.Link, new() { Name = "next" }).ClickAsync();
        await Expect(page).ToHaveURLAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/?page=4");
        
        // Check next button is invisible on page 21
        await page.GotoAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/?page=21");
        var next = page.GetByRole(AriaRole.Link, new() { Name = "next" });
        await Expect(next).Not.ToBeVisibleAsync();
        
        // Check url option is correct after pressing previous
        await page.GetByRole(AriaRole.Link, new() { Name = "previous" }).ClickAsync();
        await Expect(page).ToHaveURLAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/?page=20");
        
        await page.GetByRole(AriaRole.Link, new() { Name = "previous" }).ClickAsync();
        await Expect(page).ToHaveURLAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/?page=19");
        
        await page.GetByRole(AriaRole.Link, new() { Name = "previous" }).ClickAsync();
        await Expect(page).ToHaveURLAsync("https://bdsa2024group8chirprazor2025.azurewebsites.net/?page=18");
    }

    [Test]
    public async Task NextPreviousButtonsChangesPagesOnPrivateTimeline()
    {
        
    }
}


