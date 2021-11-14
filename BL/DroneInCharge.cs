using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL.BO
{
    class DroneInCharge
    {
        private int pid;
        public int id {
            get { return pid; }
            set
            {
                if (value != null)
                    pid = value;
                else
                    throw new InvalidID();
            }
        }            
        public double bettaryStatus { get; set; }
    }
}
