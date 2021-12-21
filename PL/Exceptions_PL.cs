using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class InvalidObjException : Exception
    {
        public InvalidObjException(string obj) : base(String.Format($"Incorrect {obj}")) { }
    }
}
