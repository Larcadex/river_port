using System;
using System.Collections.Generic;
using System.Timers;
using System.Threading;


namespace ships
{
    class Program
    {
        static int time = 50;

        static void Main()
        {
            System.Timers.Timer timer = new System.Timers.Timer(300);
            List<Ship> ships = new List<Ship>();
            List<Ship> docked_ships = new List<Ship>();
            List<Ship> waiter_ship = new List<Ship>();
            List<Ship> leave_ship = new List<Ship>();
            Port port = new Port(4);
            Random rand = new Random();

            for (int i = 1; i <= 30; i++)
            {
                Ship ship = new Ship($"Корабль {i}", "Маленький", 1, 40);
                ships.Add(ship);
            }

            for (int i = 31; i <= 45; i++)
            {
                Ship ship = new Ship($"Корабль {i}", "Средний", 2, 60);
                ships.Add(ship);
            }

            for (int i = 46; i <= 50; i++)
            {
                Ship ship = new Ship($"Корабль {i}", "Большой", 3, 100);
                ships.Add(ship);
            }

            ships.Sort((a, b) => rand.Next(-1, 2));

            Ship firstShip = ships.FirstOrDefault();

            timer.Elapsed += (sender, e) => OnTimerElapsed1(sender, e, firstShip, ships, waiter_ship, port, docked_ships, leave_ship);

            timer.Start();

            Console.ReadLine();
        }

        public static bool check_place(Port port, Ship ship)
        {
            bool a;
            int b = ship.get_occupied_place();
            int c = port.get_avaliable_place();
            if (c > b)
            {
                a = true;
            }
            else
            {
                a = false;
            }

            return a;
        }

        public static bool check_time(Ship ship)
        {
            bool a;
            int b = ship.get_arrival_time();
            int c = ship.get_service_time();

            if (time == b + c)
            {
                a = true;
            }
            else
            {
                a = false;
            }

            return a;

        }

        public static void ship_arrival(Port port, Ship ship, List<Ship> docked_ships, List<Ship> waiter_ship)
        {
            bool a = check_place(port, ship);
            int b = ship.get_occupied_place();
            int c = port.get_avaliable_place();

            if (a == true)
            {
                waiter_ship.Remove(ship);
                docked_ships.Add(ship);
                ship.set_arrival_time(time);
                Console.WriteLine($"{ship.get_type()} {ship.get_name()} прибыл в порт на обслуживание в {time}");
                port.set_avaliable_place(c -= b); 
            }
        }


        public static void ship_departure(Port port, Ship ship, List<Ship> docked_ships, List<Ship> leave_ship)
        {
            bool a = check_time(ship);
            int b = ship.get_occupied_place();
            int c = port.get_avaliable_place();

            if (a == true)
            {
                port.set_avaliable_place(c += b);
                Console.WriteLine($"{ship.get_type()} {ship.get_name()} уплыл в {time}");
                docked_ships.Remove(ship);
                leave_ship.Add(ship);
            }
        }

        public static void ship_wait(List<Ship> ships, List<Ship> waiter_ship)
        {
            if (time % 60 == 0 && time != 0)
            {
                if (ships.Count > 0)
                {
                    Ship ship1 = ships[0];
                    waiter_ship.Add(ship1);
                    ships.Remove(ship1);
                    Console.WriteLine($"{ship1.get_type()} {ship1.get_name()} добавлен в лист ожидания в {time}");
                }
            }
        }



        static void OnTimerElapsed1(object sender, System.Timers.ElapsedEventArgs e, Ship firstShip, List<Ship> ships, List<Ship> waiter_ship, Port port, List<Ship> docked_ships, List<Ship> leave_ship)
        {
            time += 10;

            ship_wait(ships, waiter_ship);

            foreach (var a in waiter_ship)
            {
                ship_arrival(port, a, docked_ships, waiter_ship);

            }
            foreach (var a in docked_ships)
            {
                ship_departure(port, a, docked_ships, leave_ship);

            }

            
        }
    }
}



