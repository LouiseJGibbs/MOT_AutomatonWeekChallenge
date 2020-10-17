Feature: Contact Us
	I want to be able to contact the business

Scenario Outline: Complete Contact Us Form with valid settings
When I submit the following contact details <name>, <email>, <phone>, <subject> and <message>
Then I should be told that the form was submitted
Examples: 
 | name | email         | phone       | subject | message                                |
 | Test | test@test.com | 01234567890 | Testing | Hello World, can I book a room please? |

