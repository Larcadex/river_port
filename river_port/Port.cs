using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ships
{
    class Port
    {
        private int avaliable_place;

        public Port(int avaliable_place)
        {
            this.avaliable_place = avaliable_place;
        }

        public int get_avaliable_place()
        {
            return avaliable_place;
        }

        public void change_place_plus(int ship_place)
        {
            avaliable_place =+ ship_place;
        }
        
        public void change_place_minus(int ship_place)
        {
            avaliable_place =- ship_place;
        }
        
    }
}