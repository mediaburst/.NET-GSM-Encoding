GSM Encoding class for C#
-------------------------

Converts characters between the .NET internal Unicode encoding and the GSM03.38 alphabet.

Licensed for use under the ISC license, a full copy can be found at the end of this file.

Sample for converting GSM encoding to UTF-8
-------------------------------------------

string body = "GSM 03.38 Text to convert";

Encoding gsmEnc = new Mediaburst.Text.GSMEncoding();
Encoding utf8Enc = new System.Text.UTF8Encoding();

byte[] gsmBytes = utf8Enc.GetBytes(body);
byte[] utf8Bytes = Encoding.Convert(gsmEnc, utf8Enc, gsmBytes);
body = utf8Enc.GetString(utf8Bytes);


ISC License
-----------

Copyright (c) 2010 Mediaburst Ltd <hello@mediaburst.co.uk>

Permission to use, copy, modify, and/or distribute this software for any
purpose with or without fee is hereby granted, provided that the above
copyright notice and this permission notice appear in all copies.

THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
