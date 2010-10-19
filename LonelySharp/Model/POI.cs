using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp.Deserializers;

namespace LonelySharp.Model
{

    public class POI : LonelySharpBase
    {
        [DeserializeAs(Name = "id")]
        public int ID { get; set; }
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }
        [DeserializeAs(Name = "digital-latitude")]
        public double? Latitude { get; set; }
        [DeserializeAs(Name = "digital-longitude")]
        public double? Longitude { get; set; }

        [DeserializeAs(Name = "alt-name")]
        public string AltName { get; set; }

        [DeserializeAs(Name = "emails")]
        public List<Email> Emails { get; set; }

        [DeserializeAs(Name = "hours")]
        public string Hours { get; set; }

        [DeserializeAs(Name = "price-range")]
        public string PriceRange { get; set; }

        [DeserializeAs(Name = "urls")]
        public List<Url> Urls { get; set; }

        [DeserializeAs(Name = "poi-type")]
        public LonelySharp.POIType POIType { get; set; }

        [DeserializeAs(Name = "address")]
        public Address Address { get; set; }
        [DeserializeAs(Name = "review")]
        public string Review { get; set; }

        [DeserializeAs(Name = "telephones")]
        public List<Telephone> Telephones { get; set; }

        [DeserializeAs(Name = "transports")]
        public List<Transport> Transports { get; set; }

        [DeserializeAs(Name = "representations")]
        public List<Representation> Representations { get; set; }
        
   //<telephones>
   //  <telephone>
   //    <area-code/>
   //    <number>77288</number>
   //    <text>tel, info</text>
   //  </telephone>
   //</telephones>
   //<transports>
   //</transports>
   //<representations>
   //  <representation type="msite" href="http://m.lonelyplanet.com/et-1000587244"/>
   //  <representation type="msite.touch" href="http://touch.lonelyplanet.com/et-1000587244"/>
   //  <representation type="lp.com" href="http://www.lonelyplanet.com/poiRedirector?poiId=363499"/>
   //</representations>

    }

    public class POICollection : List<POI>
    {
    }

    public class Representation
    {
        [DeserializeAs(Name = "type", Attribute = true)]
        public string Type { get; set; }
        [DeserializeAs(Name = "href", Attribute = true)]
        public string Href { get; set; }
    }
    public class Address
    {
        [DeserializeAs(Name = "street")]
        public string Street { get; set; }
        [DeserializeAs(Name = "locality")]
        public string Locality { get; set; }
        [DeserializeAs(Name = "postcode")]
        public string Postcode { get; set; }
        [DeserializeAs(Name = "extras")]
        public string Extras { get; set; }
    }
    public class Telephone
    {

        [DeserializeAs(Name = "area-code")]
        public string AreaCode { get; set; }
        [DeserializeAs(Name = "number")]
        public string Number { get; set; }
        [DeserializeAs(Name = "text")]
        public string Text { get; set; }
    }

    public class Transport
    {
        [DeserializeAs(Name = "transport-type")]
        public string TransportType { get; set; }
        [DeserializeAs(Name = "description")]
        public string Description { get; set; }
    }

    
    public class Email
    {
        [DeserializeAs(Name = "email")]
        public string Value { get; set; }
    }

    public class Url
    {
        public string Value { get; set; }
    }
}
