Feature: AdminInbox
	As an admin user, I want to read messages customers have sent me 

Scenario Outline: Read message sent via Contact Us form
When I submit the following contact details <name>, <email>, <phone>, <subject> and <message>
When I login to the admin section
And view the email inbox
Then I can see the message in the list of unread messages
When I click on the message
Then the message window should appear containing the expected message details

Examples: 
 | name | email         | phone       | subject | message                                |
 | Test | test@test.com | 01234567890 | Testing | Hello World, can I book a room please? |