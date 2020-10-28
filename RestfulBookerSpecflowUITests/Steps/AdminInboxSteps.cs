using OpenQA.Selenium;
using RestfulBookerSpecflowUITests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using Xunit;

namespace RestfulBookerSpecflowUITests
{
    [Binding]
    public sealed class AdminInboxSteps
    {
        public TestData _testData;

        public IWebElement MessagesInboxLink => TestUtilities.Driver.FindElement(By.XPath("//i[@class='fa fa-inbox']"));
        public IWebElement Messages => TestUtilities.Driver.FindElement(By.XPath("//div[@class='messages']"));
        public ReadOnlyCollection<IWebElement> MessagesList_All => Messages.FindElements(By.XPath(".//div[starts-with(@class,'row detail')]"));
        public ReadOnlyCollection<IWebElement> MessagesList_Read => Messages.FindElements(By.XPath(".//div[@class='row detail read-true']"));
        public ReadOnlyCollection<IWebElement> MessagesList_Unread => Messages.FindElements(By.XPath(".//div[@class='row detail read-false']"));
        public IWebElement MessageFromList;


        public By MessageWindowBy = By.XPath("//div[@class = 'ReactModal__Content ReactModal__Content--after-open message-modal']");
        public IWebElement MessageWindow => TestUtilities.Driver.FindElement(MessageWindowBy);
        public ReadOnlyCollection<IWebElement> MessageDetails => MessageWindow.FindElements(By.XPath("./div[@class='form-row']"));

        public AdminInboxSteps(TestData testData)
        {
            _testData = testData;
        }

        [When(@"view the email inbox")]
        public void WhenViewTheEmailInbox()
        {
            MessagesInboxLink.Click();
        }

        [Then(@"I can see the message in the list of unread messages")]
        public void ThenICanSeeTheNameAndSubjectInTheListOfUnreadMessages()
        {
            bool found = false;

            for (int i = MessagesList_Unread.Count; i > 0; i--)
            {
                IWebElement message = MessagesList_Unread[i - 1];
                string text = message.Text;

                if (text.Contains(_testData.MyMessage.Name) && text.Contains(_testData.MyMessage.Subject))
                {
                    found = true;
                    MessageFromList = message;
                    break;
                }
            }

            Assert.True(found, "The message could not be found in the list of read messages");
        }

        [Then(@"I can see the message in the list of read messages")]
        public void ThenICanSeeTheNameAndSubjectInTheListOdReadMessages()
        {
            bool found = false;

            for (int i = MessagesList_Read.Count; i > 0; i--)
            {
                IWebElement message = MessagesList_Read[i - 1];
                string text = message.Text;

                if (text.Contains(_testData.MyMessage.Name) && text.Contains(_testData.MyMessage.Subject))
                {
                    found = true;
                    MessageFromList = message;
                    break;
                }
            }

            Assert.True(found, "The message could not be found in the list of Unread messages");
        }

        //[Then(@"I can see the (.*) and (.*) in the list of messages")]
        //public void ThenICanSeeTheNameAndSubjectInTheListOfMessages(string name, string subject)
        //{
        //    bool found = false;

        //    for(int i = MessagesList_Unread.Count; i > 0; i--)
        //    {
        //        IWebElement message = MessagesList_Unread[i - 1];
        //        string text = message.Text;

        //        if (text.Contains(name) && text.Contains(subject))
        //        {
        //            found = true;
        //            MessageFromList = message;
        //            break;
        //        }
        //    }

        //    Assert.True(found, "The message could not be found in the list of messages");
        //}

        [When(@"I click on the message")]
        public void WhenIClickOnTheMessage()
        {
            MessageFromList.Click();
        }

        [Then(@"the message window should appear containing the expected message details")]
        public void ThenDetailsOfTheMessageCanBeViewed()
        {
            Assert.True(TestUtilities.Exists(MessageWindowBy), "The message window is not available");

            string textSearchString = ".//*[text()='{0}']";

            By PhoneNumberText = By.XPath(String.Format(textSearchString, _testData.MyMessage.PhoneNumber));
            By NameText = By.XPath(String.Format(textSearchString, _testData.MyMessage.Name));
            By EmailText = By.XPath(String.Format(textSearchString, _testData.MyMessage.Email));
            By SubjectText = By.XPath(String.Format(textSearchString, _testData.MyMessage.Subject));
            By MessageText = By.XPath(String.Format(textSearchString, _testData.MyMessage.Message));

            Assert.True(TestUtilities.Exists(MessageDetails[0], PhoneNumberText), "The phone number was not found in the expected location");
            Assert.True(TestUtilities.Exists(MessageDetails[0], NameText), "The name was not found in the expected location");
            Assert.True(TestUtilities.Exists(MessageDetails[1], EmailText), "The email was not found in the expected location");
            Assert.True(TestUtilities.Exists(MessageDetails[2], SubjectText), "The subject was not found in the expected location");
            Assert.True(TestUtilities.Exists(MessageDetails[3], MessageText), "The message was not found in the expected location");
        }

        //[Then(@"the message window appears with the (.*), (.*), (.*), (.*) and (.*)")]
        //public void ThenDetailsOfTheMessageCanBeViewed(string name, string phone, string email, string subject, string message)
        //{
        //    Assert.True(TestUtilities.Exists(MessageWindowBy), "The message window is not available");

        //    string textSearchString = ".//*[text()='{0}']";

        //    By PhoneNumberText = By.XPath(String.Format(textSearchString, phone));
        //    By NameText = By.XPath(String.Format(textSearchString, name));
        //    By EmailText = By.XPath(String.Format(textSearchString, email));
        //    By SubjectText = By.XPath(String.Format(textSearchString, subject));
        //    By MessageText = By.XPath(String.Format(textSearchString, message));

        //    Assert.True(TestUtilities.Exists(MessageDetails[0], PhoneNumberText), "The phone number was not found in the expected location");
        //    Assert.True(TestUtilities.Exists(MessageDetails[0], NameText), "The name was not found in the expected location");
        //    Assert.True(TestUtilities.Exists(MessageDetails[1], EmailText), "The email was not found in the expected location");
        //    Assert.True(TestUtilities.Exists(MessageDetails[2], SubjectText), "The subject was not found in the expected location");
        //    Assert.True(TestUtilities.Exists(MessageDetails[3], MessageText), "The message was not found in the expected location");
        //}

    }
}
