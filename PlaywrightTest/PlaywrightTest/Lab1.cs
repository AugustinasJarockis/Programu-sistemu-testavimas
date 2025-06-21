using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;

namespace PlaywrightTest
{
    public class Lab1
    {
        [Fact]
        public async Task Test() {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions {
                Headless = false,
            });
            var context = await browser.NewContextAsync();

            var page = await context.NewPageAsync();
            await page.GotoAsync("https://demowebshop.tricentis.com/");
            await page.GetByRole(AriaRole.Link, new() { Name = "Gift Cards" }).Nth(1).ClickAsync();
            await page.Locator("xpath=//div[@class='product-item']/div[@class='details' and ./div[@class='add-info']/div[@class='prices']/span/text() > 99.00]/h2/a").ClickAsync();
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Recipient's Name:" }).FillAsync("Jonas");
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Your Name:" }).FillAsync("Petras");
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Qty:" }).FillAsync("5000");
            await page.Locator("#add-to-cart-button-4").ClickAsync();
            await Assertions.Expect(page.Locator("#topcartlink")).ToContainTextAsync("(5000)");
            await page.GetByRole(AriaRole.Button, new() { Name = "Add to wishlist" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Jewelry" }).Nth(1).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Create Your Own Jewelry", Exact = true }).ClickAsync();
            await page.Locator("#product_attribute_71_9_15").SelectOptionAsync(new[] { "47" });
            await page.Locator("#product_attribute_71_10_16").FillAsync("80");
            await page.GetByRole(AriaRole.Listitem).Filter(new() { HasText = "None" }).ClickAsync();
            await page.GetByRole(AriaRole.Radio, new() { Name = "Star" }).CheckAsync();
            await page.GetByRole(AriaRole.Textbox, new() { Name = "Qty:" }).FillAsync("26");
            await page.Locator("#add-to-cart-button-71").ClickAsync();
            await Assertions.Expect(page.Locator("#topcartlink")).ToContainTextAsync("(5026)");
            await page.GetByRole(AriaRole.Button, new() { Name = "Add to wishlist" }).ClickAsync();
            await page.GetByRole(AriaRole.Link, new() { Name = "Wishlist (5026)" }).ClickAsync();
            await page.GetByRole(AriaRole.Row, new() { Name = "Picture of $100 Physical Gift" }).Locator("input[name=\"addtocart\"]").CheckAsync();
            await page.GetByRole(AriaRole.Row, new() { Name = "Picture of Create Your Own" }).Locator("input[name=\"addtocart\"]").CheckAsync();
            await page.GetByRole(AriaRole.Button, new() { Name = "Add to cart" }).ClickAsync();
            await Assertions.Expect(page.Locator("body")).ToContainTextAsync("1002600.00");
        }
    }
}