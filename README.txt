.NET wrapper for Lonely Planet REST API

Uses RestSharp: http://restsharp.org

Lonely Planet Documentation: http://developer.lplabs.com/index.php?title=Main_Page

Sample Usage:

using LonelySharp;
var lonelySharp = new LonelySharp("accountSid", "secretKey");
var places = lonelySharp.GetPlace("Brisbane");
var pointsOfInterest = lonelySharp.GetPOIList(places.First());