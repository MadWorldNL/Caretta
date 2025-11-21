Feature: Company
    
This feature covers creating and managing companies in the game.

@integration
Scenario: Found a new company
    Given I have chosen the company name "Caretta"
    When I found the company
    Then the company "Caretta" should be created