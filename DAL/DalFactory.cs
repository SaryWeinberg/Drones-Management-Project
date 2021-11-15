using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL.DO;

namespace DAL
{  
    public static class DalFactory
    {
        public static IDal factory(string type)
        {
            switch (type)
            {
                case "object":
                    return DalObject.DalObject.GetInstance;
                case "xml":
                   // return DalXml.GetInstance;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
