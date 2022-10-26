using BlazorServerPlaywright.Tests.Infrastructure;
using Xunit;

namespace BlazorServerPlaywright.Tests;

public class BlazorUiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly string _serverAddress;

    public BlazorUiTests(CustomWebApplicationFactory fixture)
    {
        _serverAddress = fixture.ServerAddress;
    }

    [Fact]
    public async Task CounterStartsWithZero()
    {
        var page = await Helpers.GetPlaywrightBrowserPage();

        // call to the `/counter` page
        await page.GotoAsync($"{_serverAddress}counter");

        // search for the counter value
        var content = await page.TextContentAsync("p");

        Assert.True(content.Equals("Current count: 0"));
    }

    [Fact]
    public async Task TableDatesStartTomorrowAscending()
    {
        var page = await Helpers.GetPlaywrightBrowserPage();

        await page.GotoAsync($"{_serverAddress}fetchdata");

        // get number of table rows
        string date1 = await page.Locator("//table/tbody/tr[1]/td[1]").InnerTextAsync();
        string date2 = await page.Locator("//table/tbody/tr[2]/td[1]").InnerTextAsync();

        Assert.True(date1.Equals(DateTime.Now.AddDays(1).ToString("MM/dd/yyyy")));
        Assert.True(date2.Equals(DateTime.Now.AddDays(2).ToString("MM/dd/yyyy")));
    }

    [Fact]
    public async Task TableHeaderHas16pxFont()
    {
        var page = await Helpers.GetPlaywrightBrowserPage();

        // call to the `/fetchdata` page
        await page.GotoAsync($"{_serverAddress}fetchdata");

        int tableRows = await page.EvalOnSelectorAsync<int>("//table", "tbl => tbl.rows.length");

        Assert.True(tableRows.Equals(6));
    }
}