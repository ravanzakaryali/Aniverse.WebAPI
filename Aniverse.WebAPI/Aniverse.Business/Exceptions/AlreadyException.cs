using System;

namespace Aniverse.Business.Exceptions
{
    public class AlreadyException : Exception
    {
        public AlreadyException(string message) : base(message) { }
    }
}
