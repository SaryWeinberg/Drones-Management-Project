using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class DroneInCharge
    {


        private int id;
        private double batteryStatus;
        public DateTime? DroneEnterToCharge { get; set; }
        public int ID {
            get { return id; }
            set
            {
                if (value >= 0)
                    id = value;
                else
                    throw new InvalidObjException("ID");
            }
        }
        public double BatteryStatus {
            get { return batteryStatus; }
            set
            {
                if (value > 0)
                    batteryStatus = value;
                else
                    throw new InvalidObjException("batteryStatus");
            }
        }

        public DroneInCharge(int DroneID, double BatteryStatus)
        {
            id = DroneID;
            batteryStatus = BatteryStatus;
            DroneEnterToCharge = DateTime.Now; 
        }

        public override string ToString()
        {
            return "ID: " + ID + " BatteryStatus: " + BatteryStatus;
        }
    }
}
