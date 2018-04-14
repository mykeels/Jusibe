# Jusibe .NET Client Library

This a Rest API Client Library based on the Official API Documentation provided by [Jusibe](https://jusibe.com/docs/), a Nigerian SMS Service.

## Installation

```bash
dotnet add package Jusibe
```

## Usage

A Client class provides three public methods for accessing the API. Instantianting the class is as given below:

```cs
using Jusibe;
using Jusibe.Models;

JusibeClient client = new JusibeClient(new SMSConfig() {
    AccessToken = System.Environment.GetEnvironmentVariable("Jusibe_Token"),
    PublicKey = System.Environment.GetEnvironmentVariable("Jusibe_Key")
});
```

## What can you do with this?

With a Jusibe Client, you can ...

### Send SMS

```cs
var result = client.Send(new RequestModel() {
    From = "mykeels",
    To = System.Environment.GetEnvironmentVariable("Phone"),
    Message = "Hello World"
}).Result;

Console.WriteLine(result.Status);
Console.WriteLine(result.MessageId);
Console.WriteLine(result.SmsCredits); // credits used to send the SMS
```

### Get SMS Credits

```cs
var result = client.GetCredits().Result;

Console.WriteLine(result.SmsCredits);
```

### Check Delivery Status

This gives you information on the delivery status of previous sent messages.

```cs
var result = client.GetDeliveryStatus("message_id").Result;

Console.WriteLine(result.SmsCredits);

## Want to Contribute?
You are free to fork this repo and make pull requests to enhance the functionalities of this library.

### How you can thank me

- Follow [@mykeels](https://twitter.com/mykeels) on twitter
- Star this github repo
- Check out my [other projects](https://github.com/mykeels) and see if you like them
- Provide useful critism. I would love to hear from you, really

Thanks, Ikechi Michael I.

### License
The MIT License (MIT). Please see License File for more information.
