using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;
using Xunit;

namespace RestfulBookerSpecflowUITests
{
    [Binding]
    public sealed class ContactUsSteps
    {
        private readonly TestData _testData = new TestData();

        public IWebElement ContactSection => TestUtilities.Driver.FindElement(By.XPath(".//div[@class='row contact']//div[@class='col-sm-5']"));


        //Contact form
        public By FormSectionBy => By.XPath("//form");
        public IWebElement FormSection => ContactSection.FindElement(FormSectionBy);
        public IWebElement NameTextBox => FormSection.FindElement(By.Id("name"));
        public IWebElement EmailTextBox => FormSection.FindElement(By.Id("email"));
        public IWebElement PhoneTextBox => FormSection.FindElement(By.Id("phone"));
        public IWebElement SubjectTextBox => FormSection.FindElement(By.Id("subject"));
        public IWebElement MessageTextBox => FormSection.FindElement(By.Id("description"));
        public IWebElement SubmitButton => FormSection.FindElement(By.Id("submitContact"));

        //Confirmation message

        public IWebElement HeaderText => ContactSection.FindElement(By.XPath(".//h2"));
        public IReadOnlyCollection<IWebElement> ParagraphText => ContactSection.FindElements(By.XPath(".//p"));

        public ContactUsSteps(TestData testData)
        {
            _testData = testData;
        }

        [When(@"I submit the following contact details (.*), (.*), (.*), (.*) and (.*)")]
        public void WhenISubmitTheFollowingContactDetailsTestTestTest_ComTestingAndHelloWorldCanIBookARoomPlease(string name, string email, string phone, string subject, string message)
        {
            _testData.MyMessage.Name = name;
            _testData.MyMessage.Email = email;
            _testData.MyMessage.PhoneNumber = phone;
            _testData.MyMessage.Subject = subject;
            _testData.MyMessage.Message = message;

            NameTextBox.SendKeys(_testData.MyMessage.Name);
            EmailTextBox.SendKeys(_testData.MyMessage.Email);
            PhoneTextBox.SendKeys(_testData.MyMessage.PhoneNumber);
            SubjectTextBox.SendKeys(_testData.MyMessage.Subject);
            MessageTextBox.SendKeys(_testData.MyMessage.Message);

            TestUtilities.ScrollToElement(SubmitButton);
            SubmitButton.Click();
        }


        [When(@"I submit some details in the contact details form")]
        public void WhenISubmitSomeDetailsInTheContactDetailsForm()
        {
            _testData.MyMessage.Name = "";
            _testData.MyMessage.Email = "";
            _testData.MyMessage.PhoneNumber = "";
            _testData.MyMessage.Subject = "";
            _testData.MyMessage.Message = "";

            NameTextBox.SendKeys(_testData.MyMessage.Name);
            EmailTextBox.SendKeys(_testData.MyMessage.Email);
            PhoneTextBox.SendKeys(_testData.MyMessage.PhoneNumber);
            SubjectTextBox.SendKeys(_testData.MyMessage.Subject);
            MessageTextBox.SendKeys(_testData.MyMessage.Message);

            TestUtilities.ScrollToElement(SubmitButton);
            SubmitButton.Click();
        }

        [Then(@"I should be told that the form was submitted")]
        public void ThenIShouldBeToldThatTheFormWasSubmitted()
        {
            Assert.True(TestUtilities.Exists(FormSectionBy));
            Thread.Sleep(2000);

            Assert.Equal(String.Format("Thanks for getting in touch {0}!", _testData.MyMessage.Name), HeaderText.Text);

            string paragraph = "";
            foreach (IWebElement p in ParagraphText)
                paragraph += p.Text + " ";

            Assert.Equal(String.Format("We'll get back to you about {0} as soon as possible. ", _testData.MyMessage.Subject), paragraph);
        }
    }
}
