using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using RestSharp.Deserializers;

namespace LonelySharp
{
    public partial class LonelySharp
    {
        private const string BaseUrl = "http://apigateway.lonelyplanet.com/api";
        private RestClient _client;

        private readonly string _oAuthKey;
        private readonly string _oAuthSecret;

        /// <summary>
        /// The number of requests that have been made by the current Client instance
        /// </summary>
        public int RequestCount { get; set; }
        /// <summary>
        /// The total Bytes returned from the requests made by the current Client instance
        /// </summary>
        public long DataCount { get; set; }

        public LonelySharp()
        {
            _client = new RestClient(BaseUrl);
            _client.ClearHandlers();
            _client.AddHandler("*", new XmlAttributeDeserializer());
        }

        public LonelySharp(string oAuthKey, string oAuthSecret)
		{
            _oAuthKey = oAuthKey;
            _oAuthSecret = oAuthSecret;

            _client = new RestClient(BaseUrl);
            _client.Authenticator = new HttpBasicAuthenticator(_oAuthKey, _oAuthSecret);
            _client.ClearHandlers();
            _client.AddHandler("*", new XmlAttributeDeserializer());
        }

		public T Execute<T>(RestRequest request) where T : new()
		{
			var response = _client.Execute<T>(request);
            RequestCount++;
            DataCount += response.RawBytes.Length;
			return response.Data;
		}

		public RestResponse Execute(RestRequest request)
		{
            var response = _client.Execute(request);
            RequestCount++;
            DataCount += response.RawBytes.Length;
		    return response;
		}
    }
}
