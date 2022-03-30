using Aniverse.Business.Exceptions.FileExceptions;

namespace Aniverse.Business.Exceptions.FileExceptions
{
    public class FileSizeException : FileException
    {
        public FileSizeException(string message) : base(message) { }
    }
}
