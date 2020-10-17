using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestfulBookerSpecflowUITests
{
    public static class TestUtilities
    {
        public static int Timeout = 40;

        public static IWebDriver Driver { get; set; }

        public static void InitializeTest()
        {
            Driver = new ChromeDriver(Environment.CurrentDirectory);
            Driver.Navigate().GoToUrl("https://aw1.automationintesting.online/");
            Driver.Manage().Window.Maximize();

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Timeout);
        }



        public static void ScrollToElement(IWebElement element)
        {
            var yPos = element.Location.Y;
            var windowSize = Driver.Manage().Window.Size.Height;
            var scrollPosition = yPos - (windowSize / 2);
            ((IJavaScriptExecutor)Driver).ExecuteScript("window.scrollTo(0, arguments[0]);", scrollPosition);
        }

        public static bool Exists(By elementBy)
        {
            try
            {
                return Driver.FindElement(elementBy).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static bool Exists(IWebElement baseElement, By elementBy)
        {
            try
            {
                return baseElement.FindElement(elementBy).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
