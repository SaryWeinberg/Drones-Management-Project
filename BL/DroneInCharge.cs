using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class DroneInCharge
    {
        private int id;
        public int ID {


            get { return id; }
            set
            {
                if (value != null)
                    id = value;
                else
                    throw new InvalidID();
            }
        }
            
        public double BettaryStatus { get; set; }
    }
}
