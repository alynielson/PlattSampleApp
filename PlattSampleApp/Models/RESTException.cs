using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PlattSampleApp.Models
{
    public class RESTException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public RESTException(string message, HttpStatusCode statusCode, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
