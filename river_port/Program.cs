using System;
using System.Collections.Generic;
using System.Timers;

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
                Ship ship = new Ship($"Корабль {i}", "Маленький", 1, 30);
                ships.Add(ship);
            }

            for (int i = 31; i <= 45; i++)
            {
                Ship ship = new Ship($"Корабль {i}", "Средний", 2, 60);
                ships.Add(ship);
            }

            for (int i = 46; i <= 50; i++)
            {
                Ship ship = new Ship($"Корабль {i}", "Большой", 3, 120);
                ships.Add(ship);
            }

            ships.Sort((a, b) => rand.Next(-1, 2));

            timer.Elapsed += (sender, e) => OnTimerElapsed1(ships, waiter_ship, port, docked_ships, leave_ship);

            timer.Start();

            Console.ReadLine();
        }

        public static void ship_wait(List<Ship> ships, List<Ship> waiter_ship)
        {
            if (time % 60 == 0 && time != 0 && ships.Count > 0)
            {    
                Ship ship1 = ships[0];
                ships.Remove(ship1);
                waiter_ship.Add(ship1);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"***[{time}] {ship1.get_type()} {ship1.get_name()} прибыл в порт и ожидает своей очереди.");
            }
        }

        public static void ship_arrival(Port port, Ship ship, List<Ship> docked_ships, List<Ship> waiter_ship)
        {
            if (port.get_avaliable_place() >= ship.get_occupied_place())
            {
                waiter_ship.Remove(ship);
                docked_ships.Add(ship);
                ship.set_arrival_time(time);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[{time}] {ship.get_type()} {ship.get_name()} причалил для обслуживания.");

                port.set_avaliable_place(port.get_avaliable_place() - ship.get_occupied_place());
            }
        }


        public static void ship_departure(Port port, Ship ship, List<Ship> docked_ships, List<Ship> leave_ship)
        {
            if (time == ship.get_arrival_time() + ship.get_service_time())
            {
                port.set_avaliable_place(port.get_avaliable_place() + ship.get_occupied_place());

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[{time}] {ship.get_type()} {ship.get_name()} был обслужен и уплыл.");

                docked_ships.Remove(ship);
                leave_ship.Add(ship);
            }
        }

        static void OnTimerElapsed1(List<Ship> ships, List<Ship> waiter_ship, Port port, List<Ship> docked_ships, List<Ship> leave_ship)
        {
            time += 10;

            ship_wait(ships, waiter_ship);

            foreach (var a in waiter_ship.ToArray()) 
            {
                ship_arrival(port, a, docked_ships, waiter_ship);
            }

            // Обработка ушедших кораблей
            foreach (var a in docked_ships.ToArray()) 
            {
                ship_departure(port, a, docked_ships, leave_ship);
            }
        }

    }
}



