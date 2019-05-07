using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS
{
    public class InvalidCategoryException : Exception
    {
        public InvalidCategoryException()
        {
        }

        public InvalidCategoryException(string message)
            : base(message)
        {
        }

        public InvalidCategoryException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
