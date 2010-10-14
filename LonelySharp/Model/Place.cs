using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp.Deserializers;

namespace LonelySharp.Model
{

    public class Place : LonelySharpBase
    {
        [DeserializeAs(Name = "id")]
        public int ID { get; set; }
        [DeserializeAs(Name = "full-name")]
        public string Fullname { get; set; }
        [DeserializeAs(Name = "short-name")]
        public string Shortname { get; set; }
        [DeserializeAs(Name = "north-latitude")]
        public double? Northlatitude { get; set; }
        [DeserializeAs(Name = "south-latitude")]
        public double? Southlatitude { get; set; }
        [DeserializeAs(Name = "east-longitude")]
        public double? Eastlongitude { get; set; }
        [DeserializeAs(Name = "west-longitude")]
        public double? Westlongitude { get; set; }
    }

    public class PlaceCollection : List<Place>
    {
        //[DeserializeAs(Name = "places")]
        //public List<Place> Places { get; set; }
    }
}
