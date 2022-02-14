using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PL
{
    public delegate void ObjectChangedAction<T>(T objectChanged);

    public static  class tostring
    {
        public static string ToSortableString(this DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd");
        }
    }
} 

