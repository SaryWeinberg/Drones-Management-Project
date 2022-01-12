using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace DalApi
{  
    public static class DalFactory
    {
        public static IDal GetDal()
        {
            string dalType = DalConfig.DalName;
            string dalPkg = DalConfig.DalPackages[dalType];

            if (dalPkg == null) throw new DalConfigException($"Package {dalType} is not fount in package list in dal-config.xml");
            
            try { Assembly.Load(dalPkg); }
            catch (Exception) { throw new DalConfigException("Failed to load the dal-config.xml file");}
            
            Type type = Type.GetType($"DAL.{dalPkg}, {dalPkg}");
            
            if (type == null) throw new DalConfigException($"Class {dalPkg} was not fount in the {dalPkg}.dll");
            
            IDal dal = (IDal)type.GetProperty("GetInstance", BindingFlags.Public | BindingFlags.Static).GetValue(null);

            if (dal == null) throw new DalConfigException($"Class {dalPkg} is not a singelton or wrong property name for Instance");

            return dal;
        }
    }
}
