using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace SeleniumTestProject
{
    public class Lab4 : IDisposable
    {
        IWebDriver driver = new ChromeDriver();
        [Fact]
        public void Lab4Test() {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            Console.WriteLine("Navigating...");
            driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");
            driver.Manage().Window.Maximize();

            driver.FindElement(By.XPath("//div[@class='leftside-3']/div/div/ul/li/a[@href='/jewelry']")).Click();
            js.ExecuteScript("window.scrollBy(0,200)", "");
            driver.FindElement(By.XPath("//div[@data-productid='14']//a[1]")).Click();
            driver.FindElement(By.XPath("//div[@class='compare-products']/input")).Click();

            for (int i = 1; i <= 4; i++) {
                driver.FindElement(By.XPath("//div[@class='side-2']/div/div/ul/li/a[@href='/books']")).Click();
                js.ExecuteScript("window.scrollBy(0,200)", "");
                driver.FindElement(By.XPath("//div[@class='product-grid']/div[" + i + "]/div/div/h2/a")).Click();
                driver.FindElement(By.XPath("//div[@class='compare-products']/input")).Click();
            }

            var compareTableElements = driver.FindElements(By.XPath("//table[@class='compare-products-table']/tbody/tr[1]/td"));

            Assert.Equal(4, compareTableElements.Count - 1);

            var lastElement = driver.FindElement(By.XPath("//table[@class='compare-products-table']/tbody/tr[2]/td[5]/a"));

            Assert.Equal("Computing and Internet", lastElement.Text);
        }

        public void Dispose() {
            driver.Close();
        }
    }
}
