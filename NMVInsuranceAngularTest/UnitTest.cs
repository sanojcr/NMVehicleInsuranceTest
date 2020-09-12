using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


namespace NMVInsuranceAngularTest
{
    [TestClass]
    public class UnitTest
    {
        // Type your valid test user credentials
        string validUserName="anu@gmail.com";
        string validPassword="sanoj12";

        string invalidUserName = "anu@g";
        string invalidPassword = "saa";

        // Selenium driver
        public IWebDriver driver;
        public UnitTest()
        {
            driver = new ChromeDriver();
            driver.Url = "http://localhost:4200";
        }

        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) { }
        [ClassCleanup()]
        public static void MyClassCleanup() { }

        [TestInitialize()]
        public void MyTestInitialize() 
        {
            driver.Manage().Window.Maximize();
        }
        [TestCleanup()]
        public void MyTestCleanup() 
        {
            driver.Close();
        }

        [TestMethod]
        public void ClientValidLogin()
        {
            driver.FindElement(By.XPath("//*[@href='/login']")).Click();
            
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.XPath("//*[@name='email']")).SendKeys(this.validUserName);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.XPath("//*[@name='pass']")).SendKeys(this.validPassword);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.XPath("//*[@type='submit']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//li/a[@id='logout']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Console.WriteLine("Client valid login test passed successfully");
        }
        [TestMethod]
        public void ClientInvalidLogin()
        {
            driver.FindElement(By.XPath("//*[@href='/login']")).Click();
            //Thread.Sleep(10000);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.XPath("//*[@name='email']")).SendKeys(this.invalidUserName);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.XPath("//*[@name='pass']")).SendKeys(this.invalidPassword);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.FindElement(By.XPath("//*[@type='submit']")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Thread.Sleep(2000);
            bool result = driver.FindElement(By.XPath("//small/p[text()='Invalid email or password']")).Displayed;
            Assert.IsTrue(result);
            Console.WriteLine("Client invalid login test passed successfully");
        }
    }
}
