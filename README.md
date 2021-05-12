# Jusibe .NET Client Library

This a Rest API Client Library based on the Official API Documentation provided by [Jusibe](https://jusibe.com/docs/), a Nigerian SMS Service.

## Installation

```bash
dotnet add package Jusibe
```

## Usage

First, in your `appsettings.json` file, create a new section for Jusibe as follows:
```json
"Jusibe": {
    "Key": "Your API Key here",
    "Token": "You can get both the Key and Token from your Jusibe Dashboard"
}
```
Then using the client is as simple as adding this line to your ConfigureServices method in your Startup.cs file:

```cs
using Jusibe;

public void ConfigureServices(IServiceCollection services)
{
    // other configured services ommitted for brevity
    services.AddJusibeClient(Configuration);
}
```

And then the client can be added wherever it is needed via Dependency Injection. For example: 
```cs
public class SmsService
{
    private readonly IJusibeClient _jusibeClient;

    public SmsService(IJusibeClient jusibeClient)
    {
        _jusibeClient = jusibeClient;
    }
    ...
    // Rest of the class goes here as usual
}
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

[@allengblack](https://twitter.com/allengblack) made some significant updates to this. Follow him on Twitter, too and you can see some more of his work [here](https://github.com/allengblack)


### License
The MIT License (MIT). Please see License File for more information.
