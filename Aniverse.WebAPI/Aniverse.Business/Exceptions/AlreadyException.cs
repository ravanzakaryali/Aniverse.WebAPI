using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Business.Exceptions
{
    public class AlreadyException : Exception
    {
        public AlreadyException(string message) : base(message) { }
    }
}
