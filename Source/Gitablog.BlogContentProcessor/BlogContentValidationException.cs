using System;

namespace Gitablog.BlogContentProcessor
{
    internal class BlogContentValidationException : Exception
    {
        public BlogContentValidationException(string msg) : base(msg)
        {
            
        }
    }
}