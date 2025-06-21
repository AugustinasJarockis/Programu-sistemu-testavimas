using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTestProject
{
    public class AccountCreation {
        public string password = "qwerty";
        public string email = "";
        public AccountCreation() {
            IWebDriver driver = new ChromeDriver();
            Console.WriteLine("Navigating...");
            driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");
            driver.Manage().Window.Maximize();

            driver.FindElement(By.XPath("//a[@href='/login']")).Click();
            driver.FindElement(By.XPath("//input[@value='Register']")).Click();
            driver.FindElement(By.Id("gender-male")).Click();
            driver.FindElement(By.Id("FirstName")).SendKeys("Petras");
            driver.FindElement(By.Id("LastName")).SendKeys("Petraitis");
            email = "petriukas" + DateTime.Now.ToString().Replace(" ", "").Replace(":", "") + "@gmail.com";
            driver.FindElement(By.Id("Email")).SendKeys(email);
            driver.FindElement(By.Id("Password")).SendKeys(password);
            driver.FindElement(By.Id("ConfirmPassword")).SendKeys(password);
            driver.FindElement(By.Id("register-button")).Click();
            driver.FindElement(By.XPath("//input[@value='Continue']")).Click();
            driver.Close();
        }
    }
    public class Lab3 : IClassFixture<AccountCreation>
    {
        AccountCreation accountCreation;

        public Lab3 (AccountCreation accountCreation) {
            this.accountCreation = accountCreation;
        }

        public static IEnumerable<object[]> TestData() {
            yield return new object[] { "C:\\Users\\jarau\\source\\repos\\SeleniumUniversityLabs\\SeleniumTestProject\\data1.txt" };
            yield return new object[] { "C:\\Users\\jarau\\source\\repos\\SeleniumUniversityLabs\\SeleniumTestProject\\data2.txt" };
        }

        [Theory, MemberData(nameof(TestData))]
        public void Lab3Test(string path) {
            IWebDriver driver = new ChromeDriver();
            try {
                Console.WriteLine("Navigating...");
                driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                driver.Manage().Window.Maximize();

                driver.FindElement(By.XPath("//a[@href='/login']")).Click();
                driver.FindElement(By.Id("Email")).SendKeys(accountCreation.email);
                driver.FindElement(By.Id("Password")).SendKeys(accountCreation.password);
                driver.FindElement(By.XPath("//input[@value='Log in']")).Click();
                
                driver.FindElement(By.XPath("//div[@class='leftside-3']/div/div/ul/li/a[@href='/digital-downloads']")).Click();

                FileStream data = File.Open(path, FileMode.Open);

                StreamReader file = new StreamReader(data);

                string? productName = file.ReadLine();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

                string oldCartQuantity = "(0)";

                while (productName != null) {
                    driver.FindElements(By.XPath("//div[@class='details' and ./h2/a/text() = '" + productName + "']/div/div/input"))[0].Click();

                    var cartQuantity = driver.FindElement(By.ClassName("cart-qty"));
                    wait.Until(condition => {
                        if (cartQuantity.Text != oldCartQuantity) {
                            oldCartQuantity = cartQuantity.Text;
                            return true;
                        }
                        return false;
                    });

                    productName = file.ReadLine();
                }

                data.Close();

                driver.FindElement(By.XPath("//li[@id='topcartlink']/a")).Click();
                driver.FindElement(By.Id("termsofservice")).Click();
                driver.FindElement(By.Id("checkout")).Click();

                if (driver.FindElements(By.Id("billing-address-select")).Count == 0) {
                    var selectCountry = driver.FindElement(By.Id("BillingNewAddress_CountryId"));
                    var select = new SelectElement(selectCountry);
                    select.SelectByValue("156");

                    driver.FindElement(By.Id("BillingNewAddress_City")).SendKeys("Palanga");
                    driver.FindElement(By.Id("BillingNewAddress_Address1")).SendKeys("Basanavičiaus g. 15");
                    driver.FindElement(By.Id("BillingNewAddress_ZipPostalCode")).SendKeys("555555");
                    driver.FindElement(By.Id("BillingNewAddress_PhoneNumber")).SendKeys("+370111111111");
                }

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollBy(0,200)", "");

                driver.FindElement(By.XPath("//div[@id='billing-buttons-container']/input")).Click();
                driver.FindElement(By.XPath("//div[@id='payment-method-buttons-container']/input")).Click();
                driver.FindElement(By.XPath("//div[@id='payment-info-buttons-container']/input")).Click();
                driver.FindElement(By.XPath("//div[@id='confirm-order-buttons-container']/input")).Click();

                var orderCompletionMessage = driver.FindElement(By.XPath("//div[@class='section order-completed']/div[@class='title']/strong"));

                Assert.Equal("Your order has been successfully processed!", orderCompletionMessage.Text);
            }
            finally {
                Console.WriteLine("Closing...");
                driver.Close();
            }
        }
    }
}