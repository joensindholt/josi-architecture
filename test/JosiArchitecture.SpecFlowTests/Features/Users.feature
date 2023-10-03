Feature: Get Users

Can we create a user and get the user back

Scenario: Get all users
    Given I add a user named 'John Doe'
	Then I get a list of users containing one named 'John Doe'
