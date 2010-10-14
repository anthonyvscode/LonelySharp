using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LonelySharp.Model;
using RestSharp;

namespace LonelySharp
{
    public partial class LonelySharp
    {
        public PlaceCollection FindPlace(string place)
        {
            var request = new RestRequest {Resource = "places"};
            request.AddParameter("name", place);
            return Execute<PlaceCollection>(request);
        }
    }
}
