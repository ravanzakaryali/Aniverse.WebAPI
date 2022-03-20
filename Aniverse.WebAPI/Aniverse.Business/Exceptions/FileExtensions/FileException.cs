using System;

namespace Aniverse.Business.Exceptions.FileExtensions
{
    public class FileException : Exception
    {
        public FileException(string message) : base(message) { }
    }
}
