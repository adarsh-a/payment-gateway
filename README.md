# payment-gateway
Payment Gateway Challenge

Implementation of a simple Payment Gateway using .Net Core 2.2

#Assumptions
The payment gateway has access to the following informations:
 - merchants identifier
 - merchants card number

#Improvements
 - Make use of private shared key for authentication between Bank and Payment Gateway
 - Use encryption for sensitive data
 - Add logging for all errors found when performing any action
 - Store data in MongoDB
 - Use async calls for API
 - Add validations for card number
 - Use Provider pattern for different banks


#API Testing
1. Get Transactions for a specific merchant.

`/api/paymentgateway/gettransactiondetails
Headers:
 Content-type : application/json
Body: 
 "a731bea3-d82f-4cdb-8405-c91e4a4603f4"

 
 Merchant Identifier is passed in the body.
`

2. Process a transaction
`/api/paymentgateway/makepayment
Headers:
 Content-type : application/json
Body: 
	{
	"merchantid": "a731bea3-d82f-4cdb-8405-c91e4a4603f4",
	"payercardnum":"14815619",
	"amount":"112.0",
	"ccv":"12354",
	"expirydate":"11/12/2019"
	}

`