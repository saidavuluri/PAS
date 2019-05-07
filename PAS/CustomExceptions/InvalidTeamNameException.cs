using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAS.CustomExceptions
{
    public class InvalidTeamNameException: Exception
    {
        public InvalidTeamNameException()
        {
        }

        public InvalidTeamNameException(string message)
            : base(message)
        {
        }

        public InvalidTeamNameException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
