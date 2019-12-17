# Payment-gateway
Payment Gateway Challenge

Implementation of a simple Payment Gateway using .Net Core 2.2

## Assumptions
The payment gateway has access to the following informations:
 - merchants identifier
 - merchants card number

## Improvements
 - Make use of private shared key for authentication between Bank and Payment Gateway
 - Use encryption for sensitive data
 - Store data in MongoDB
 - Use async calls for API
 - Use Provider pattern for different banks


## API Testing
1. Get Transactions for a specific merchant.
````
/api/paymentgateway/{merchantId}
Headers:
 Content-type : application/json


 
 Merchant Identifier is passed in the body.
````

2. Process a transaction
````
/api/paymentgateway
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

````

## Update

1. Add swagger for API docs
2. Create a domain for card logic
3. Use Serilog to write logs to ElasticSearchSink
4. Use Kibana to query the logs
5. Write unit test for card validation
