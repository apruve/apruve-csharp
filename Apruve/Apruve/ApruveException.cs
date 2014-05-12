using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apruve
{
    public class ApruveException : Exception
    {
        public string message { get; set; }
        public ApruveException(string message, Exception cause)
            : base(message, cause)
        {

        }

        public ApruveException(string message)
            : base(message)
        {

        }

    }
}