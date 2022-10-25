using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace BlazorServerPlaywright.Tests;

public class MainPageTests : PlaywrightTest
{
    private IBrowser browser;

    [SetUp]
    public async Task SetUp()
    {
        browser = await Playwright.Chromium.LaunchAsync(new()
        {
            Headless = false // forc a browser window
        });
    }

    [Test]
    public async Task CounterStartsWithZero()
    {
        var page = await browser.NewPageAsync();
        await page.GotoAsync("http://localhost:5233/counter");

        // search for the counter value
        var content = await page.TextContentAsync("p");

        Assert.That(content, Is.EqualTo("Current count: 0"));
    }
}

/*
public class MainPageTests : PageTest
{
    [Test]
    public async Task CounterStartsWithZero()
    {
        // call to the `/counter` page
        await Page.GotoAsync("http://localhost:5233/counter");

        // search for the counter value
        var content = await Page.TextContentAsync("p");

        Assert.That(content, Is.EqualTo("Current count: 1")); // <-- Compares a, b
    }
}
*/