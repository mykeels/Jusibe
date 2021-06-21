# Jusibe .NET Client Library

This a Rest API Client Library based on the Official API Documentation provided by [Jusibe](https://jusibe.com/docs/), a Nigerian SMS Service.

## Installation

```bash
dotnet add package Jusibe
```

## Usage
### To use this client in a .NET Core application:

1. First, in your `appsettings.json` file, create a new section for Jusibe.

```json
"Jusibe": {
    "Key": "Your API Key here",
    "Token": "You can get both the Key and Token from your Jusibe Dashboard",
    "BaseAddress" : "Jusibe Base Address. (It's probably 'https://jusibe.com/smsapi')"
}
```
2. Add this snippet to your ConfigureServices method in your Startup.cs file:
```cs
using Jusibe;

public void ConfigureServices(IServiceCollection services)
{
    // other services ommitted for brevity
    services.AddJusibeClient(Configuration);
}
```

And then the client can be added wherever it is needed via Dependency Injection: 
```cs
public class SmsService
{
    private readonly IJusibeClient _jusibeClient;

    public SmsService(IJusibeClient jusibeClient)
    {
        _jusibeClient = jusibeClient;
    }
    ...
    // Rest of your code here...
}
```

### To use this client in a .NET CLI appication:

```cs
using Jusibe;

var jusibeClient = new JusibeClient(new JusibeClientOptions
{
    Key = "Your Jusibe Key Here",
    Token = "Your Jusibe Token Here",
    BaseAddress = "https://jusibe.com/smsapi"
});
```

## What can you do with this?

With this Jusibe Client, you can ...

### Send SMS

```cs
ResponseModel result = await jusibeClient.SendSms(new RequestModel() {
    From = "mykeels",
    To = "08012345678",
    Message = "Hello World"
});
```

### Get SMS Credits

```cs
CreditModel result = await client.GetCredits();
```

### Check Delivery Status

This gives you information on the delivery status of previous sent messages.

```cs
DeliveryStatusModel result = await client.GetDeliveryStatus("message_id");
```

### Send Bulk SMS

Lets you send Bulk sms:

```cs
BulkResponseModel result = await client.SendBulkSms(new BulkRequestModel() {
    From = "mykeels",
    To = "08012345678,08012345678", // phone numbers separated by commas
    Message = "Hello World"
});
```

### Check Bulk SMS Delivery Status

This gives you information on the delivery status of previous sent bulk message.

```cs
BulkStatusResponseModel result = await client.GetBulkSmsStatus("bulk_message_id");
```

## Want to Contribute

You are free to fork this repo and make pull requests to enhance the functionalities of this library.

### How you can thank us

- Follow [@mykeels](https://twitter.com/mykeels) on twitter
- Star this github repo
- Check out my [other projects](https://github.com/mykeels) and see if you like them
- Provide useful critism. I would love to hear from you, really

Thanks, Ikechi Michael I.

PS.
[@allengblack](https://github.com/allengblack) made some significant updates to this. Follow him on [Twitter](https://twitter.com/allengblack), too!


### License
The MIT License (MIT). Please see License File for more information.
