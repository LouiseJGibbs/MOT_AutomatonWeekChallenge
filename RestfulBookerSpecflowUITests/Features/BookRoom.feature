Feature: BookRoom
	As an customer, I want to be able to book a room in the hotel

Scenario: Book a room
Given at least 1 room exists in the hotel
When I click on the book a room button
Then the room info section should appear
When I select a date range
And I enter valid room booking details
Then I should see the successful booking message



	