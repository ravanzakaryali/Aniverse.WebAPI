using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Business.Exceptions.DateTimeExceptions
{
    public class OneStoryPerDayException : DateException
    {
        public OneStoryPerDayException(string message) : base(message)
        {
        }
    }
}
