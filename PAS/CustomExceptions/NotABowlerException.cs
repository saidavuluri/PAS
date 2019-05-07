using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.CustomExceptions
{
    public class NotABowlerException: Exception
    {
        public NotABowlerException()
        {
        }

        public NotABowlerException(string message)
            : base(message)
        {
        }

        public NotABowlerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
