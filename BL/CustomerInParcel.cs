using System;

namespace IBL.BO
{
    public class CustomerInParcel
    {       
        private int id;
        private string name;

        public int ID {
            get { return id; }
            set
            {
                if (value > 100000000 && value < 999999999 && BL.BL.ValidateIDNumber(id))
                    id = value;
                else
                    throw new InvalidObjException("ID");
            }
        }

        public string Name {
            get { return name; }
            set
            {
                if (value  != null)
                    name = value;
                else
                    throw new InvalidObjException("ID");
            }
        }

        public override string ToString()
        {
            return " id: " + ID + " name: " + Name ;
        }
    }
}
