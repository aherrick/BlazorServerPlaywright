using Microsoft.Playwright;

namespace BlazorServerPlaywright.Tests;

public static class Helpers
{
    public static async Task<IPage> GetPlaywrightBrowserPage(int slowMo = 2000)
    {
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new()
        {
            Headless = false,
            SlowMo = slowMo
        });

        var context = await browser.NewContextAsync(new BrowserNewContextOptions { IgnoreHTTPSErrors = true });

        var page = await context.NewPageAsync();

        return page;
    }
}