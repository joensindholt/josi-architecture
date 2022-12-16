Feature: TodoList

A short summary of the feature

Scenario: Get all todo lists
    Given I add a todo list named 'My Todo List'
	When I request all todo lists
	Then I get a list of todo lists containing one name 'My Todo List'
