using System;

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

        public void set_avaliable_place(int setter)
        {
            avaliable_place = setter;
        }

    }
}