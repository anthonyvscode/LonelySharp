using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

namespace LonelySharp.Model
{
    public class TooManyResultsException : Exception
    {
        public int ResultCount { get; set; }

        public TooManyResultsException()
        {
            
        }

        public TooManyResultsException(string message) : base(message)
        {

        }
    }
}
