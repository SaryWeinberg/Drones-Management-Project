using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*using IDAL;*/
using DalApi;


namespace DalObject
{  
    public static class DalFactory
    {
        public static IDal GetDal(string type)
        {
            switch (type)
            {
                case "object":
                    return DalObject.GetInstance;
                case "xml":
                   // return DalXml.GetInstance;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
