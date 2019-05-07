using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.CustomExceptions
{
    public class NotABatsmanException : Exception
    {
        public NotABatsmanException()
        {
        }

        public NotABatsmanException(string message)
            : base(message)
        {
        }

        public NotABatsmanException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
