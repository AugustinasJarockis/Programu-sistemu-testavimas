using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTestProject
{
    public class Lab1
    {
        [Fact]
        public void Lab1Test() {
            IWebDriver driver = new ChromeDriver();

            Console.WriteLine("Navigating...");
            driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");

            var giftCardLink = driver.FindElement(By.XPath("//a[normalize-space() = \"Gift Cards\"]"));
            giftCardLink.Click();

            var selectedGiftCard = driver.FindElement(
                By.XPath("//div[@class='product-item']/div[@class='details' and ./div[@class='add-info']/div[@class='prices']/span/text() > 99.00]/h2/a")
                );
            selectedGiftCard.Click();

            var recipientName = driver.FindElement(By.Id("giftcard_4_RecipientName"));
            recipientName.SendKeys("Petras");

            var senderName = driver.FindElement(By.Id("giftcard_4_SenderName"));
            senderName.SendKeys("Jonas");

            var quantity = driver.FindElement(By.Id("addtocart_4_EnteredQuantity"));
            quantity.Clear();
            quantity.SendKeys("5000");

            var addToCartButton = driver.FindElement(By.Id("add-to-cart-button-4"));
            addToCartButton.Click();

            var addToWishlistButton = driver.FindElement(By.Id("add-to-wishlist-button-4"));

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            var cartQuantity = driver.FindElement(By.ClassName("cart-qty"));
            wait.Until(condition => {
                if (cartQuantity.Text != "(0)") {
                    return true;
                }
                return false;
            });

            addToWishlistButton.Click();
            var wishlistQuantity = driver.FindElement(By.ClassName("wishlist-qty"));
            wait.Until(condition => {
                if (wishlistQuantity.Text != "(0)") {
                    return true;
                }
                return false;
            });

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0,-100)", "");

            var jewelryButton = driver.FindElement(By.XPath("//div[@class='block block-category-navigation']/div[@class='listbox']/ul/li/a[normalize-space()='Jewelry']"));
            jewelryButton.Click();

            var createJewelryLink = driver.FindElement(By.XPath("//a[normalize-space()='Create Your Own Jewelry']"));
            createJewelryLink.Click();

            var selectElement = driver.FindElement(By.Id("product_attribute_71_9_15"));
            var select = new SelectElement(selectElement);
            select.SelectByValue("47");

            var jewelryLength = driver.FindElement(By.Id("product_attribute_71_10_16"));
            jewelryLength.Clear();
            jewelryLength.SendKeys("80");

            var starPendant = driver.FindElement(By.Id("product_attribute_71_11_17_50"));
            starPendant.Click();

            var jewelryQuantity = driver.FindElement(By.Id("addtocart_71_EnteredQuantity"));
            jewelryQuantity.Clear();
            jewelryQuantity.SendKeys("26");

            var addToCartJewelry = driver.FindElement(By.Id("add-to-cart-button-71"));
            addToCartJewelry.Click();

            cartQuantity = driver.FindElement(By.ClassName("cart-qty"));
            wait.Until(condition => {
                if (cartQuantity.Text == "(5026)") {
                    return true;
                }
                return false;
            });

            var addToWishlistJewelry = driver.FindElement(By.Id("add-to-wishlist-button-71"));
            addToWishlistJewelry.Click();

            var wishlistLink = driver.FindElement(By.ClassName("ico-wishlist"));
            js.ExecuteScript("window.scrollBy(0,-1000)", "");
            wishlistLink.Click();

            var addToCartCheckboxes = driver.FindElements(By.Name("addtocart"));
            foreach (var checkbox in addToCartCheckboxes) {
                checkbox.Click();
            }

            var addToCartButtonInWishlist = driver.FindElement(By.Name("addtocartbutton"));
            addToCartButtonInWishlist.Click();

            var subtotalAmount = driver.FindElement(
                By.XPath(
                    "//table[@class='cart-total']/tbody/tr[./td[@class='cart-total-left']/span='Sub-Total:']/td[@class='cart-total-right']/span/span"
                    )
                );

            Console.WriteLine("Found sub-total: " + subtotalAmount.Text);
            Assert.Equal(subtotalAmount.Text, "1002600.00");

            Console.WriteLine("Closing...");
            driver.Close();
        }
    }
}