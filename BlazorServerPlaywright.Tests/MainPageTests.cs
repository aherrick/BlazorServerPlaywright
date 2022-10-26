using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace BlazorServerPlaywright.Tests;

public class MainPageTests : PageTest
{
    [Test]
    public async Task CounterStartsWithZero()
    {
        // call to the `/counter` page
        await Page.GotoAsync("http://localhost:5233/counter");

        // search for the counter value
        var content = await Page.TextContentAsync("p");

        Assert.That(content, Is.EqualTo("Current count: 0"));
    }

    [Test]
    public async Task TableDatesStartTomorrowAscending()
    {
        // call to the `/fetchdata` page
        await Page.GotoAsync("http://localhost:5233/fetchdata");

        // get number of table rows
        string date1 = await Page.Locator("//table/tbody/tr[1]/td[1]").InnerTextAsync();
        string date2 = await Page.Locator("//table/tbody/tr[2]/td[1]").InnerTextAsync();

        Assert.Multiple(() =>
        {
            // assertion for the value
            Assert.That(date1, Is.EqualTo(DateTime.Now.AddDays(1).ToString("MM/dd/yyyy")));
            Assert.That(date2, Is.EqualTo(DateTime.Now.AddDays(2).ToString("MM/dd/yyyy")));
        });
    }

    [Test]
    public async Task TableHeaderHas16pxFont()
    {
        // call to the `/fetchdata` page
        await Page.GotoAsync("http://localhost:5233/fetchdata");

        int tableRows = await Page.EvalOnSelectorAsync<int>("//table", "tbl => tbl.rows.length");

        Assert.That(tableRows, Is.EqualTo(6));
    }
}