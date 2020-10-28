using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace RestfulBookerSpecflowUITests
{
    [Binding]
    public sealed class NavigationSteps
    {
        public IWebElement LetMeHackButton => TestUtilities.Driver.FindElement(By.XPath("//button[text()='Let me hack!']"));
        public IWebElement Footer => TestUtilities.Driver.FindElement(By.Id("footer"));
        public IWebElement AdminLink => Footer.FindElement(By.XPath(".//a[@href='/#/admin']"));
        public IWebElement UsernameTextbox => TestUtilities.Driver.FindElement(By.Id("username"));
        public IWebElement PasswordTextbox => TestUtilities.Driver.FindElement(By.Id("password"));
        public IWebElement LoginButton => TestUtilities.Driver.FindElement(By.Id("doLogin"));

        [BeforeScenario]
        public void StartScenario()
        {
            TestUtilities.InitializeTest();
            LetMeHackButton.Click();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            TestUtilities.Driver.Close();
        }

        [When(@"I login to the admin section")]
        public void WhenILoginToTheAdminSection()
        {
            AdminLink.Click();

            UsernameTextbox.SendKeys("admin");
            PasswordTextbox.SendKeys("password");
            LoginButton.Click();
        }
    }
}
