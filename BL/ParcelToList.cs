using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    public class ParcelToList
    {
        public int id { get; set; }
        public string senderName { get; set; }
        public string targetName { get; set; }
        public WeightCategories weight { get; set; }
        public Priorities priority { get; set; }
        public Status status { get; set; }
    }
}
