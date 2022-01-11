using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLApi;

namespace BL
{
    public static class BLFactory
    {
        public static IBL GetBL()
        {
            return BL.GetInstance;
        }
    }
}



