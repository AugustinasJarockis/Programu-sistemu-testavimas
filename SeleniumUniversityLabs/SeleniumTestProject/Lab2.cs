using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Firefox;

namespace SeleniumTestProject
{
    public class Lab2
    {
        [Fact]
        public void Lab2Test1() {
            IWebDriver driver = new FirefoxDriver();
            
            try {
                Console.WriteLine("Navigating...");
                driver.Navigate().GoToUrl("https://demoqa.com/");

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                driver.Manage().Window.Maximize();

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollBy(0,200)", "");

                var widgetsButton = driver.FindElement(By.XPath("//div[@class='card-body']/h5[text()='Widgets']"));
                widgetsButton.Click();

                js.ExecuteScript("window.scrollBy(0,400)", "");
                var progressBar = driver.FindElement(By.XPath("//li[@id='item-4' and ./span/text()='Progress Bar']"));
                progressBar.Click();

                var startStopButton = driver.FindElement(By.Id("startStopButton"));
                startStopButton.Click();

                var progressBarValue = driver.FindElement(By.XPath("//div[@id='progressBar']/div"));
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(40));
                wait.Until(condition => {
                    if (progressBarValue.Text == "100%") {
                        return true;
                    }
                    return false;
                });

                js.ExecuteScript("window.scrollBy(0,200)", "");
                var resetButton = driver.FindElement(By.Id("resetButton"));
                resetButton.Click();

                Console.WriteLine("Current bar value: " + progressBarValue.Text);
                Assert.Equal("0%", progressBarValue.Text);
            }
            finally {
                Console.WriteLine("Closing...");
                driver.Close();
            }  
        }

        [Fact]
        public void Lab2Test2() {
            IWebDriver driver = new FirefoxDriver();

            try {
                Console.WriteLine("Navigating...");
                driver.Navigate().GoToUrl("https://demoqa.com/");

                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                driver.Manage().Window.Maximize();

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("window.scrollBy(0,100)", "");

                var widgetsButton = driver.FindElement(By.XPath("//div[@class='card-body']/h5[text()='Elements']"));
                widgetsButton.Click();

                var webTables = driver.FindElement(By.XPath("//li[@id='item-3' and ./span/text()='Web Tables']"));
                webTables.Click();

                var addButton = driver.FindElement(By.Id("addNewRecordButton"));


                var pageCount = driver.FindElement(By.XPath("//span[@class='-totalPages']"));

                while (pageCount.Text == "1") {

                    addButton.Click();

                    var firstName = driver.FindElement(By.Id("firstName"));
                    firstName.SendKeys("Petras");

                    var lastName = driver.FindElement(By.Id("lastName"));
                    lastName.SendKeys("Petraitis");

                    var userEmail = driver.FindElement(By.Id("userEmail"));
                    userEmail.SendKeys("petriuko@chata.lt");

                    var age = driver.FindElement(By.Id("age"));
                    age.SendKeys("9");

                    var salary = driver.FindElement(By.Id("salary"));
                    salary.SendKeys("20");
            
                    var department = driver.FindElement(By.Id("department"));
                    department.SendKeys("Fortnite");

                    var submitButton = driver.FindElement(By.Id("submit"));
                    submitButton.Click();
                }

                js.ExecuteScript("window.scrollBy(0,600)", "");
                var nextButton = driver.FindElement(By.XPath("//div[@class='-next']/button"));
                nextButton.Click();

                var deleteButton = driver.FindElement(By.Id("delete-record-11"));
                deleteButton.Click();

                var firstElementDeleteButtonCount = driver.FindElements(By.XPath("//span[@id='delete-record-1']")).Count;

                Assert.Equal(1, firstElementDeleteButtonCount);
                Assert.Equal("1", pageCount.Text);


                var pageNr = driver.FindElement(By.XPath("//div[@class='-pageJump']/input"));
            
                Assert.Equal("1", pageNr.GetAttribute("value"));
            }
            finally {
                Console.WriteLine("Closing...");
                driver.Close();
            }
        }
    }
}