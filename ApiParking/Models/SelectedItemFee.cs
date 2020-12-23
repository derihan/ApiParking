using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiParking.Models
{
    public class SelectedItemFee
    {
        private int id;

        public int ParkFeesId
        {
            get { return id; }
            set { id = value; }
        }

        private int _value;

        public int ParkFeesValue
        {
            get { return _value; }
            set { _value = value; }
        }


    }
}
