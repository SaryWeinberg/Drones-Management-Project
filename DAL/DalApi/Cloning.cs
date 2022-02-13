using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public static class Cloning
    {
        public static T Clone<T>(this T original) where T : new()
        {
            T newObject = new T();
            foreach (var originalProp in original.GetType().GetProperties())
            {
                originalProp.SetValue(newObject, originalProp.GetValue(original));
            }
            return newObject;
        }
    }
}
