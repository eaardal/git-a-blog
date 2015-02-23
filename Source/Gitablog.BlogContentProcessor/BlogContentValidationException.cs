using System;

namespace Gitablog.BlogContentProcessor
{
    public class BlogContentValidationException : Exception
    {
        public BlogContentValidationException(string msg) : base(msg)
        {
            
        }
    }
}