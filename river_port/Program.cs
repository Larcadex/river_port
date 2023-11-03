using System;
using System.Collections.Generic;
using System.Timers;
using System.Threading;




namespace ships
{
    class Program
    {
        static int time = 0;
        static int tickCounter = 0;

        static void Main()

        {

            System.Timers.Timer timer = new System.Timers.Timer(500);
            
            List<Ship> ships = new List<Ship>();
            List<Ship> docked_ships = new List<Ship>();

            List<Ship> waiter_ship = new List<Ship>();
            List<Ship> leave_ship = new List<Ship>();
            Port port = new Port(4);
            Random rand = new Random();

            // изменить на 50!
            for (int i = 1; i <= 50; i++)
            {
                Ship ship = new Ship($"Корабль {i}", "Маленький", 1, 40);
                ships.Add(ship);
            }

            //// Create 15 medium ships
            //for (int i = 31; i <= 45; i++)
            //{
            //    Ship ship = new Ship($"Корабль {i}", "Средний", 2, 60);
            //    ships.Add(ship);
            //}

            //// Create 20 large ships
            //for (int i = 46; i <= 50; i++)
            //{
            //    Ship ship = new Ship($"Корабль {i}", "Большой", 3, 100);
            //    ships.Add(ship);
            //}

            /*foreach (Ship ship in ships)
            {
                Console.WriteLine($"Ship Name: {ship.get_name()}, Type: {ship.get_type()}");
            }*/

            for (int i = 0; ships.Count > 0 || waiter_ship.Count > 0 || docked_ships.Count > 0; i++)
            {
                time ++;
                Console.WriteLine(time);

                if (time % 60 == 0 && time != 0)
                {
                    Console.WriteLine("Прошел час");
                    if (ships.Count > 0)
                    {
                        Ship ship1 = ships[0];
                        waiter_ship.Add(ship1);
                        ships.Remove(ship1);
                        Console.WriteLine($"{ship1.get_type()} {ship1.get_name()} добавлен в лист ожидания в {time}");

                    }
                }

                int g = 0;
                while (g < waiter_ship.Count)
                {
                    Ship ship2 = waiter_ship[g];
                    ship_arrival(port, ship2);
                    waiter_ship.Remove(ship2);
                    docked_ships.Add(ship2);
                }

                for (int j = 0; j < docked_ships.Count; j++)
                {
                    Ship ship3 = docked_ships[j];
                    ship_departure(port, ship3);
                    docked_ships.Remove(ship3);
                    leave_ship.Add(ship3);
                }



                //if (docked_ships.Count > 0)
                //{
                //    Ship ship3 = docked_ships[0];
                //    ship_departure(port, ship3, docked_ships, leave_ship);
                //}
            }

            //Ship firstShip = ships.FirstOrDefault();
            //timer.Elapsed += (sender, e) =>
            //    OnTimerElapsed1(sender, e, firstShip, ships, waiter_ship, port, docked_ships, leave_ship);

            //timer.Start();



            //----------------------------------------

            Console.ReadLine();
        }

        public static void ship_arrival(Port port, Ship ship)
        {

            bool a = check_place(port, ship);
            int b = ship.get_occupied_place();
            if (a == true)
            {
                ship.arrival_time = time;
                Console.WriteLine($"{ship.get_type()} {ship.get_name()} прибыл в порт в {ship.arrival_time}");
                port.change_place_minus(b);
            }
            else
            {
                Console.WriteLine($"{ship.get_type()} {ship.get_name()} не прибыл в порт");

            }
        }


        public static bool check_place(Port port, Ship ship)
        {
            bool a;
            if (port.get_avaliable_place() > ship.get_occupied_place())
            {
                a = true;
            }
            else
            {
                a = false;
            }
            
            return a;
        }

        public static bool check_time(Port port, Ship ship)
        {
            //int serviceTime = ship.get_service_time();

            bool a;
            if (time == ship.arrival_time + ship.service_time)
            {
                a = true;
            }
            else
            {
                a = false;
            }

            return a;

        }

        public static void ship_departure(Port port, Ship ship)
        {
            int a = ship.get_occupied_place();

            if (time >= ship.arrival_time + ship.service_time)
            {
                port.change_place_plus(a);
                Console.WriteLine($"{ship.get_type()} {ship.get_name()} уплыл в {time}");
            }
        }



        //static void OnTimerElapsed1(object sender, System.Timers.ElapsedEventArgs e, Ship firstShip, List<Ship> ships,
        //    List<Ship> waiter_ship, Port port, List<Ship> docked_ships, List<Ship> leave_ship)
        //{
        //    time += 10;
        //    Console.WriteLine(time);

        //    if (time % 60 == 0 && time != 0)
        //    {
        //        Console.WriteLine("Прошел час");
        //        if (ships.Count > 0)
        //        {
        //            Ship ship1 = ships[0];
        //            waiter_ship.Add(ship1);
        //            ships.Remove(ship1);
        //            Console.WriteLine($"{ship1.get_type()} {ship1.get_name()} добавлен в лист ожидания в {time}");

        //        }
        //    }

        //    for (int i = 0; docked_ships.Count >= 0; i++)
        //    {
        //        Ship ship3 = docked_ships[i];
        //        ship_arrival(port, ship3, waiter_ship, docked_ships);
        //    }

        //    for (int i = 0; waiter_ship.Count >= 0; i++)
        //    {
        //        Ship ship2 = waiter_ship[i];
        //        ship_arrival(port, ship2, waiter_ship, docked_ships);
        //    }

        //    //if (docked_ships.Count > 0)
        //    //{
        //    //    Ship ship3 = docked_ships[0];
        //    //    ship_departure(port, ship3, docked_ships, leave_ship);
        //    //}






        //}
    }
}



