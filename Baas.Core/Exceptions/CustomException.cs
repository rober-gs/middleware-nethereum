using System;
using System.Collections.Generic;
using System.Text;

namespace Baas.Core.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException()
        {
                
        }

        public CustomException(string message) : base(message)
        {

        }
    }
}
