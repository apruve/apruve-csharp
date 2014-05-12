apruve-csharp
=============

C# client library for interacting with the Apruve API.

A NuGet package exists for this library, and can be found [here](https://www.nuget.org/packages/Apruve/).



## Issues

Please use [Github issues](https://github.com/apruve/apruve-csharp/issues) to request features or report bugs.

## Usage

Before using the rest of the Apruve C# client, be sure to call the ApruveClient.init() method to configure the client with 
your API key and the environment you wish to use (ApruveEnvironment.testEnvironment() or ApruveEnvironment.prodEnvironment()).

If you need to use multiple API keys in the same application, you will need to re-init the client with the appropriate key before each call.

```
ApruveClient.init("myAPIKey", ApruveEnvironment.testEnvironment());
```

During the checkout process, the PaymentRequest class can be used to generate the JSON and secure hash strings that are needed to initialize apruve.js.  Instantiate the class, add line items, subscriptions, or other attributes, and call the
toJson() and/or toSecureHash() methods:

```
// Create line items

LineItem lineItem1 = new LineItem()
{
  title = "Letter Paper",
  description = "20 lb ream (500 Sheets).\n  Paper dimensions are 8.5 x 11.00 inches.",
  sku = "LTR-20R",
  price_ea_cents = 1200,
  quantity = 3,
  amount_cents = 3600,
  view_product_url = "https://merchant-demo.herokuapp.com"
};

LineItem lineItem2 = new LineItem()
{
  title = "Legal Paper",
  description = "24 lb ream (250 Sheets).\n  Paper dimensions are 8.5 x 14.00 inches.",
  sku = "LGL-24R",
  price_ea_cents = 950,
  quantity = 2,
  amount_cents = 1900,
  view_product_url = "https://merchant-demo.herokuapp.com"
};

// Add line items to payment request

PaymentRequest paymentRequest = new PaymentRequest()
{
    merchant_id = "YourMerchantIdHere",
    currency = "USD",
    amount_cents = 6000,
    shipping_cents = 500,
    line_items = new List<LineItem>()
    {
        lineItem1,
        lineItem2
    }
};

// return json object and hash for apruve.js 

json = paymentRequest.toJson();
hash = paymentRequest.toSecureHash();
```

Each model class defines methods that implement calls to the different RESTful APIs defined for that model.  The basic pattern is that you pass a Payment object, a PaymentRequest object, and/or a PaymentRequest object's ID to one of the API call methods on the Payment or PaymentRequest class in order to generate the request uri and body.

The requests themselves are created, executed, and validated by [RestSharp](http://restsharp.org/) and the responseHandler() methods on the ApruveClient class.

For example, 
this will retrieve and update a Payment Request:

```
PaymentRequest paymentRequest = PaymentRequest.get(paymentRequestId);
paymentRequest.amount_cents = 12345;
paymentRequest.shipping_cents = 200;
paymentRequest.update(paymentRequest);
```

API Calls either return instances of the method class, or an instance of PaymentRequestUpdateResponse. If the call fails, the system will throw an ApruveException instead of returning one of the aforementioned response objects. This ApruveException object will have the response status message as a parameter.

```
PaymentRequestUpdateResponse response = PaymentRequest.finalize(paymentRequestId);
if (response != null) {
  System.Diagnostics.Debug.WriteLine("The new status is: " + response.status);
} else {
  System.Diagnostics.Debug.WriteLine("Request Failed: " + ApruveException.message);
}
```

## Testing

This repository includes an ApruveTest project intended to be used with the Microsoft Unit Test Framework. These tests can be run with Microsoft Visual Studio's [Test Explorer](http://msdn.microsoft.com/en-us/library/hh270865.aspx).

## Contributing

1. Fork it
2. Create your feature branch (`git checkout -b my-new-feature`)
3. Write your code **and tests**
4. Ensure all [tests](#testing) still pass
5. Commit your changes (`git commit -am 'Add some feature'`)
6. Push to the branch (`git push origin my-new-feature`)
7. Create new pull request
