using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ships
{
    class Ship
    {
        protected string name;
        protected string type;
        protected int occupied_place;
        protected int service_time;
        protected int arrival_time;
        

        public Ship(string name, string type, int occupied_place, int service_time)
        {
            this.name = name;
            this.type = type;
            this.occupied_place = occupied_place;
            this.service_time = service_time;
        }

        public int get_occupied_place()
        {
            return occupied_place;
        }

        public int get_arrival_time()
        {
            return arrival_time; 
        }
        public void set_arrival_time(int setter)
        {
            arrival_time = setter;
        }

        public int get_service_time()
        {
            return service_time;
        }

        public string get_type()
        {
            return type;
        }
        
        public string get_name()
        {
            return name;
        }
        
    }
}