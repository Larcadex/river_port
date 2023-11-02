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

            System.Timers.Timer timer = new System.Timers.Timer(1000);
            /*while (true)
            {
                
            
                Console.WriteLine($"Глобальное время: {time}");
                System.Threading.Thread.Sleep(1000); // Задержка 1 секунда для наглядности
            }*/


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
            
            foreach (Ship ship in ships)
            {
                Console.WriteLine($"Ship Name: {ship.get_name()}, Type: {ship.get_type()}");
            }

            Ship firstShip = ships.FirstOrDefault();
            timer.Elapsed += (sender, e) => OnTimerElapsed1(sender, e, firstShip, ships, waiter_ship, port, docked_ships);

            //timer.Elapsed += OnTimerElapsed2;

            timer.Start();
            
            
            
            //----------------------------------------

            Console.ReadLine(); // This line will keep the console application running.
        }

        public static void ship_arrival(Port port, Ship ship, List<Ship> ships, List<Ship> docked_ships)
        {
            ship.arrival_time = time;
            Console.WriteLine($"{ship.get_type()} корабль прибыл в порт в {ship.arrival_time}");
            docked_ships.Add(ship);
            port.change_place_minus(ship.get_occupied_place());
            
        }


        public static bool check_place(Port port, Ship ship)
        {
            bool a;
            if (port.get_avaliable_place() >= ship.get_occupied_place())
            {
                a = true;
            }
            else
            {
                a = false;
            }


            return a;
        }

        public bool check_time(Port port, Ship ship)
        {
            int serviceTime = ship.get_service_time();

            bool a;
            if (time == ship.arrival_time + serviceTime)
            {
                a = true;
            }
            else
            {
                a = false;
            }


            return a;
            
        }
        
        

        public static void ship_departure(Port port, Ship ship, List<Ship> ships, List<Ship> leave_ship)
        {           

        
            ships.Remove(ship);
            port.change_place_plus(ship.get_occupied_place());
            Console.WriteLine($"{ship.get_type()} {ship.get_name()} уплыл в {time}");
            

        }


        static void OnTimerElapsed1(object sender, System.Timers.ElapsedEventArgs e, Ship firstShip, List<Ship> ships, List<Ship> waiter_ship, Port port, List<Ship> docked_ships)
        {
            lock (waiter_ship)
            {
                if (time % 60 == 0)
                {
                    Console.WriteLine("пРОШЕЛ ЧАС");
                    if (ships.Count > 0)
                    {
                        Ship ship1 = ships[0];
                        waiter_ship.Add(ship1);
                        ships.Remove(ship1);
                        Console.WriteLine($"Корабль {ship1.get_name()} добавлен в waiter_ship в {time}");

                        if (waiter_ship.Count > 0)
                        {
                            Ship ship2 = waiter_ship[0];
                            bool a = check_place(port, ship2);

                            if (a)
                            {
                                ship_arrival(port, ship2, ships, docked_ships);
                                waiter_ship.Remove(ship2);
                            }
                        }
                    }
                }
            }

            time += 10;
        }









        //static void OnTimerElapsed2(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    if (time % 60 == 0 && time != 0)
        //    {
        //        Console.WriteLine("мяууу");
        //    }

        //}
    }

}

