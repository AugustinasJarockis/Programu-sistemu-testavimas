using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace PlaywrightDemo
{
    public class Lab2
    {
        [Fact]
        public async Task Test1() {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions {
                Headless = false,
            });
            var context = await browser.NewContextAsync();

            var page = await context.NewPageAsync();
            await page.GotoAsync("https://demoqa.com/");
            await page.Locator("div").Filter(new() { HasTextRegex = new Regex("^Widgets$") }).First.ClickAsync();
            await page.GetByText("Progress Bar").ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Start" }).ClickAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Reset" }).ClickAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Progressbar)).ToContainTextAsync("0%");
        }
        [Fact]
        public async Task Test2() {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions {
                Headless = false,
            });
            var context = await browser.NewContextAsync();

            var page = await context.NewPageAsync();
            await page.GotoAsync("https://demoqa.com/");
            await page.Locator("div").Filter(new() { HasTextRegex = new Regex("^Elements$") }).First.ClickAsync();
            await page.GetByText("Web Tables").ClickAsync();
            for(int i = 0; i < 8; i++) {
                await page.GetByRole(AriaRole.Button, new() { Name = "Add" }).ClickAsync();
                await page.GetByRole(AriaRole.Textbox, new() { Name = "First Name" }).ClickAsync();
                await page.GetByRole(AriaRole.Textbox, new() { Name = "First Name" }).FillAsync("Jonas");
                await page.GetByRole(AriaRole.Textbox, new() { Name = "Last Name" }).ClickAsync();
                await page.GetByRole(AriaRole.Textbox, new() { Name = "Last Name" }).FillAsync("Jonaitis");
                await page.GetByRole(AriaRole.Textbox, new() { Name = "name@example.com" }).ClickAsync();
                await page.GetByRole(AriaRole.Textbox, new() { Name = "name@example.com" }).FillAsync("jonaitis@gmail.com");
                await page.GetByRole(AriaRole.Textbox, new() { Name = "Age" }).ClickAsync();
                await page.GetByRole(AriaRole.Textbox, new() { Name = "Age" }).FillAsync("56");
                await page.GetByRole(AriaRole.Textbox, new() { Name = "Salary" }).ClickAsync();
                await page.GetByRole(AriaRole.Textbox, new() { Name = "Salary" }).FillAsync("20000");
                await page.GetByRole(AriaRole.Textbox, new() { Name = "Department" }).ClickAsync();
                await page.GetByRole(AriaRole.Textbox, new() { Name = "Department" }).FillAsync("Testing");
                await page.GetByRole(AriaRole.Button, new() { Name = "Submit" }).ClickAsync();
            }

            await page.GetByRole(AriaRole.Button, new() { Name = "Next" }).ClickAsync();
            await page.GetByTitle("Delete").Locator("path").ClickAsync();
            await Assertions.Expect(page.GetByRole(AriaRole.Grid)).ToContainTextAsync("Cierra");
            await Assertions.Expect(page.Locator("#app")).ToContainTextAsync("1");
            await Assertions.Expect(page.GetByRole(AriaRole.Spinbutton, new() { Name = "jump to page" })).ToContainTextAsync("1");
        }
    }
}