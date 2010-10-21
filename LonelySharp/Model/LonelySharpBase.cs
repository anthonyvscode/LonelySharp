using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp.Deserializers;

namespace LonelySharp.Model
{
    public abstract class LonelySharpBase
    {
        //TODO: add in error logic
        [DeserializeAs(Name = "id")]
        public int ID { get; set; }
    }
}
