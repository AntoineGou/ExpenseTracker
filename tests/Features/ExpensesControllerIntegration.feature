Feature: ExpensesControllerIntegration

A short summary of the feature

@integration
Scenario: Add one Expense
	Given a webapi
	When I GET all Expenses
	Then status code is 200
	And Expenses are
	| Date | Amount | Recipient | Currency | Type |
	When I POST an expense
	| Date       | Recipient | Amount | Currency | Type |
	| 2022-10-01 | Kebab     | 100    | CHF      | Food |
	Then status code is 201
	When I GET all Expenses
	Then status code is 200
	And Expenses are
	| Date       | Recipient | Amount | Currency | Type |
	| 2022-10-01 | Kebab     | 100    | CHF      | Food |

@integration
Scenario: update one Expense
	Given a webapi
	When I POST an expense
	| Date       | Recipient | Amount | Currency | Type |
	| 2022-10-01 | Kebab     | 100    | CHF      | Food |
	Then status code is 201
	When I GET all Expenses
	Then status code is 200
	And Expenses are
	| Date       | Recipient | Amount | Currency | Type | Id |
	| 2022-10-01 | Kebab     | 100    | CHF      | Food | 1  |
	When I PUT Expense 1 to
	| Date       | Recipient | Amount | Currency | Type  | Id |
	| 2022-10-01 | Fuel      | 200    | USD      | Other | 1  |
	Then status code is 200
	When I GET all Expenses
	Then status code is 200
	And Expenses are
	| Date       | Recipient | Amount | Currency | Type  |
	| 2022-10-01 | Fuel      | 200    | USD      | Other |

@integration
Scenario: delete one Expense
	Given a webapi
	When I POST an expense
	| Date       | Recipient | Amount | Currency | Type |
	| 2022-10-01 | Kebab     | 100    | CHF      | Food |
	Then status code is 201
	When I GET all Expenses
	Then status code is 200
	And Expenses are
	| Date       | Recipient | Amount | Currency | Type | Id |
	| 2022-10-01 | Kebab     | 100    | CHF      | Food | 1  |
	When I DELETE Expense 1
	Then status code is 200
	When I GET all Expenses
	Then status code is 200
	And Expenses are
	| Date       | Recipient | Amount | Currency | Type  |
