# Jusibe C# Client Library
#####This a .NET C# Client Library based on the Official API Documentation provided by [Jusibe](https://jusibe.com/docs/) a Nigerian SMS Service.

## Installation
[Microsoft .NET 4.5 Framework](https://www.microsoft.com/en-us/download/details.aspx?id=30653), Visual Studio 2012 or later, [Extensions](https://github.com/mykeels/Extensions)

##Usage
A Client class provides three public methods for accessing the API. Instantianting the class is as given below:

```
Jusibe.Client client = new Jusibe.Client();
```

Its constructor may take two string parameters:

```
string publicKey = "[Enter Public Key Here]";
string accessToken = "[Enter Access Token]";

Jusibe.Client client = new Jusibe.Client(publicKey, accessToken);
```

When not provided, the parameters default to null, and the public key and access token are gotten from your App Settings. 

Make sure to add the following keys to the App Settings as given below:

```
<appSettings>
    <add key="Jusibe_Root_Url" value="https://jusibe.com/smsapi/" />
    <add key="Jusibe_Public_Key" value="" />
    <add key="Jusibe_Access_Token" value="" />
</appSettings>
```

### Jusibe Client Class Methods
Its methods are:

#### Get Credits
This makes an authentication request to the Jusibe API, and returns a `Promise<Jusibe.Models.Credit>` as Response.

##### Usage
```
Jusibe.Client client = new Jusibe.Client();
client.GetCredits().Success((Credit credit) =>
    {
        string jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(credit);
        Console.WriteLine(jsonResponse);
    }).Error((Exception ex) =>
    {
        throw ex;
    });
```

#### Send SMS
This lets you make a request to the Jusibe API, to send an SMS. It takes a `Jusibe.Models.SMS.Request` object as a parameter and returns a `Promise<Jusibe.Models.SMS.Response>` object.

##### Usage
```
SMS.Request request = new SMS.Request("[phone number here]", "[sender name here]", "[sms message here]");
client.SendSms(request).Success((SMS.Response response) =>
{
    string jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(response);
    Console.WriteLine(jsonResponse);
}).Error((Exception ex) =>
{
    throw ex;
});
```

#### Check SMS Delivery Status
This gives you information on the delivery status of previous sent messages. It takes a single paramter: `string message_id` and returns a Promise<Jusibe.Models.SMS.DeliveryStatus> object

##### Usage
```
client.CheckDelivery("nw53j49123").Success((Jusibe.Models.SMS.DeliveryStatus status) =>
{
    string jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(status);
    Console.WriteLine(jsonResponse);
}).Error((Exception ex) =>
{
    throw ex;
});
```

### Jusibe Models
You may have seen references to Classes such as `Jusibe.Models.SMS.Request` in above sections of this documents. These are references to the `Jusibe.Models` namespace. It contains classes such as:
- `Credit` class
  - `string sms_credits`
  - `string GetEndPointUrl()`
- `SMS` class
  - `Request` class
    - `string to`
    - `string from`
    - `string message`
    - `Request()`
    - `Request(string to, string from, string message)`
    - `string getAsQuery()`
  - `Response` class
    - `string status`
    - `string message_id`
    - `string sms_credits`
  - `DeliveryStatus` class
    - `string message_id`
    - `string status`
    - `string date_sent`
    - `string date_delivered`
    - `string GetEndpointUrl(string message_id)`
    
### Want to Contribute?
You are free to fork this repo and make pull requests to enhance the functionalities of this library.

### How you can thank me
Follow [@mykeels](https://twitter.com/mykeels) on twitter. Star this github repo. Check out my other projects and see if you like them.

Provide useful code critism. I'd love to hear from you, really.

Thanks! Ikechi Michael I.

### License
The MIT License (MIT). Please see License File for more information.
