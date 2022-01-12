using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class ObjectDoesNotExist : Exception
    {
        public ObjectDoesNotExist(string obj, int id) : base(String.Format($"{obj}, ID - {id} does not exist")) { }
    }

    public class XmlFileLoadCreateException : Exception
    {
        public XmlFileLoadCreateException(string exe) : base(String.Format(exe)) { }
    }
}
