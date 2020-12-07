# Rajat's submission for [Checkout.com](checkout.com)

## Purpose
This repository is housing the code for the payment gateway. It exposes the following http endpoints

`
Get Payment by Id
`
```yaml
GET api/payments/{id}
```

`
Capture new payment
`

```yaml
POST api/payments
{
  "card": {
    "number": "4111 1111 1111 1111",
    "expiry": "12/21",
    "cvv": 123
  },
  "amount": 2,
  "currency": "EUR"
}
```

## Dependencies
The application depends on 
* an api to make requests to Visa for capturing payments made by VISA cards
* an api to make requests to Mastercard for capturing payments made by MasterCard cards
* mongo db for storing the succesfully captured payments

## Tools
* Docker
To mock out dependencies during the development and debugging.

* Fiddler or your favourite tool
To make http requests to the endpoints

* MongoDBCompass
To visually browse the database

## Tests
There are 2 projects for testing the application
* Checkout.Com.PaymentGateway.Tests.Contract
This has the consumer driven contract tests for the http endpoints

* Checkout.Com.PaymentGateway.Tests.Unit
This has the unit tests for the services.

# Debugging and Development
If database and external services are still unavailable, then
```sh
cd Checkout.Com.PaymentGateway.API\MockedExternalDependencies 
docker-compose up --build
```
If database and external services are available, they are to be configured in `appsettings.json`

## Deployment
For deploying the application, the yml file has been added to the project.

## Security(Vulenrabilities)
* The endpoints are currently open and no token is required to hit them.
* There are no authorization checks on the endpoints and any one can create or view a payment