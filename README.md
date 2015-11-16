# GSM Encoding class for C# #

Converts characters between the .NET internal Unicode encoding and the GSM03.38 alphabet.

Licensed for use under the MIT license, a full copy can be found in License.txt.

## Installing

Now available as a NuGet package so just ```Install-Package Mediaburst.Text.GSMEncoding```

## Sample for converting GSM encoding to UTF-8

```csharp
string body = "GSM 03.38 Text to convert";

Encoding gsmEnc = new Mediaburst.Text.GSMEncoding();
Encoding utf8Enc = new System.Text.UTF8Encoding();

byte[] gsmBytes = utf8Enc.GetBytes(body);
byte[] utf8Bytes = Encoding.Convert(gsmEnc, utf8Enc, gsmBytes);
body = utf8Enc.GetString(utf8Bytes);
```
