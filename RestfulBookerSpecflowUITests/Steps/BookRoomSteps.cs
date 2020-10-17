
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TechTalk.SpecFlow;
using Xunit;

namespace RestfulBookerSpecflowUITests
{
    [Binding]
    public sealed class BookRoomSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestData _testData = new TestData();

        public IWebElement BookRoomButton => TestUtilities.Driver.FindElement(By.XPath("//button[@class='btn btn-outline-primary float-right openBooking']"));
        public ReadOnlyCollection<IWebElement> RoomInformation => TestUtilities.Driver.FindElements(By.XPath("//div[@class='row hotel-room-info']"));
        public int InitialRoomSectionsOnPage = 0;

        public IWebElement Calendar => TestUtilities.Driver.FindElement(By.XPath("//div[@class='rbc-calendar']"));
        public ReadOnlyCollection<IWebElement> CalendarRows => Calendar.FindElements(By.XPath(".//div[@class='rbc-month-row']"));
        public By DaysInWeek => By.XPath(".//div[@class='rbc-day-bg']");

        public IWebElement FirstNameTextbox => TestUtilities.Driver.FindElement(By.Name("firstname"));
        public IWebElement LastNameTextbox => TestUtilities.Driver.FindElement(By.Name("lastname"));
        public IWebElement EmailTextbox => TestUtilities.Driver.FindElement(By.Name("email"));
        public IWebElement PhoneTextbox => TestUtilities.Driver.FindElement(By.Name("phone"));
        public IWebElement SubmitBookingButton => TestUtilities.Driver.FindElement(By.XPath("//button[@class='btn btn-outline-primary float-right book-room']"));
        public IWebElement NextMonth => TestUtilities.Driver.FindElement(By.XPath("//button[text()='Next']"));
        public IWebElement SuccessfulBookingMessage => TestUtilities.Driver.FindElement(By.XPath("//div[@class='form-row']//h3"));

        public BookRoomSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"at least 1 room exists in the hotel")]
        public void GivenAtLeastRoomExistsInTheHotel()
        {
            Assert.True(RoomInformation.Count > 0, "No rooms currently exist in the hotel");
        }

        [When(@"I click on the book a room button")]
        public void WhenIClickOnTheBookARoomButton()
        {
            InitialRoomSectionsOnPage = RoomInformation.Count; //When room section expands, an additional section is created on the page
            BookRoomButton.Click();
        }

        [Then(@"the room info section should appear")]
        public void ThenTheRoomInfoSectionShouldAppear()
        {
            Assert.True(RoomInformation.Count > InitialRoomSectionsOnPage);
        }


        [When(@"I select a date range")]
        public void WhenISelectADateRange()
        {

            bool dateSet = false;
            bool available = false;

            while (!dateSet)
            {
                foreach (IWebElement week in CalendarRows)
                {
                    TestUtilities.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

                    try
                    {
                        available = !week.FindElement(By.XPath(".//div[@title='Unavailable']")).Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        available = true;
                    }

                    TestUtilities.Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TestUtilities.Timeout);

                    //Only book week if there are more than 1 day within the current month, and the week is available
                    if (available && week.FindElements(DaysInWeek).Count > 1)
                    {
                        TestUtilities.ScrollToElement(week);
                        SelectDateRangeInWeek(week);
                        dateSet = true;
                        break;
                    }
                }

                NextMonth.Click();
            }

        }

        public void SelectDateRangeInWeek(IWebElement week)
        {
            ReadOnlyCollection<IWebElement> days = week.FindElements(DaysInWeek);
            IWebElement day1 = days[0];
            IWebElement day2 = days[days.Count - 1];

            var action = new Actions(TestUtilities.Driver);

            action.ClickAndHold(day2);
            action.MoveToElement(day2);
            action.MoveToElement(day1);
            action.DragAndDrop(day1, day2);
            action.Perform();
        }

        [When(@"I enter valid room booking details")]
        public void WhenIEnterValidRoomBookingDetails()
        {
            FirstNameTextbox.SendKeys("Firstname");
            LastNameTextbox.SendKeys("Lastname");
            EmailTextbox.SendKeys("email@test.com");
            PhoneTextbox.SendKeys("01234567890");
            SubmitBookingButton.Click();
        }

        [Then(@"I should see the successful booking message")]
        public void ThenIShouldSeeTheSuccessfulBookingMessage()
        {
            Assert.Equal("Booking Successful!", SuccessfulBookingMessage.Text);
        }
    }
}
