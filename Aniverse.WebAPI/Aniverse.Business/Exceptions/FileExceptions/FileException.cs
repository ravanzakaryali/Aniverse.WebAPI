using System;

namespace Aniverse.Business.Exceptions.FileExceptions
{
    public class FileException : Exception
    {
        public FileException(string message) : base(message) { }
    }
}
