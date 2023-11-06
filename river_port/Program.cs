using System;
using System.Collections.Generic;
using System.Timers;
using System.Threading;


namespace ships
{
    class Program
    {
        static int time = 0;

        static void Main()
        {
            bool is_running = true;

            System.Timers.Timer timer = new System.Timers.Timer(300);

            List<Ship> ships = new List<Ship>();
            List<Ship> docked_ships = new List<Ship>();
            List<Ship> waiter_ship = new List<Ship>();
            List<Ship> leave_ship = new List<Ship>();

            Port port = new Port(4);

            Random rand = new Random();

            for (int i = 1; i <= 30; i++)
            {
                Ship ship = new Ship($"корабль {i}", "Маленький", 1, 70);
                ships.Add(ship);
            }

            for (int i = 31; i <= 45; i++)
            {
                Ship ship = new Ship($"корабль {i}", "Средний", 2, 100);
                ships.Add(ship);
            }

            for (int i = 46; i <= 50; i++)
            {
                Ship ship = new Ship($"корабль {i}", "Большой", 3, 150);
                ships.Add(ship);
            }

            ships.Sort((a, b) => rand.Next(-1, 2)); 
            
            Console.WriteLine($"Нажмите клавишу для Q паузы и вывода статистики для текущего момента (нажмите еще раз для возобновления)");
            
            timer.Elapsed += (sender, e) => OnTimerElapsed1(ships, waiter_ship, port, docked_ships, leave_ship);
            timer.Start();

            Thread status = new Thread(() => port_status(port, ships, waiter_ship, docked_ships, leave_ship));
            status.Start();


            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Q)
                {
                    if (is_running)
                    {
                        port_status(port, ships, waiter_ship, docked_ships, leave_ship);
                        timer.Stop();
                        is_running = false;
                    }
                    else
                    {
                        last_line(9);
                        timer.Start();
                        is_running = true;
                    }
                }
            }
        }

        public static void print_time()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"[{time}]");
        }

        public static void ship_wait(Port port, List<Ship> ships, List<Ship> waiter_ship)
        {
            if (time % 60 == 0 && ships.Count > 0 || time == 0)
            {    
                Ship ship1 = ships[0];
                ships.Remove(ship1);
                waiter_ship.Add(ship1);

                
                if (port.get_avaliable_place() < ship1.get_occupied_place())
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"----------------------------------------------------------------\n");
                    print_time();
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write($"{ship1.get_type()} {ship1.get_name()} прибыл в порт и ожидает своей очереди.\n");
                    Console.Write($"----------------------------------------------------------------\n");
                }

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
                Console.Write($"----------------------------------------------------------------\n");
                print_time();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{ship.get_type()} {ship.get_name()} причалил для обслуживания.\n");
                Console.Write($"----------------------------------------------------------------\n");

                port.set_avaliable_place(port.get_avaliable_place() - ship.get_occupied_place());
            }
        }


        public static void ship_departure(Port port, Ship ship, List<Ship> docked_ships, List<Ship> leave_ship)
        {
            if (time == ship.get_arrival_time() + ship.get_service_time())
            {
                port.set_avaliable_place(port.get_avaliable_place() + ship.get_occupied_place());

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"----------------------------------------------------------------\n");
                print_time();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{ship.get_type()} {ship.get_name()} был обслужен и уплыл.\n");
                Console.Write($"----------------------------------------------------------------\n");

                docked_ships.Remove(ship);
                leave_ship.Add(ship);
            }
        }

        static void port_status(Port port, List<Ship> ships, List<Ship> waiter_ship, List<Ship> docked_ships, List<Ship> leave_ship)
        {
            if (time != 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"----------------------------------------------------------------\n");
                Console.WriteLine($"Текущее состояние порта:");
                Console.WriteLine($"Текущее время: {time}");
                Console.WriteLine($"Ожидают обслуживания: {waiter_ship.Count}");
                Console.WriteLine($"Обслуживаются: {docked_ships.Count}");
                Console.WriteLine($"Уплыли: {leave_ship.Count}");
                Console.WriteLine($"Оставшиеся корабли: {ships.Count}");
                Console.WriteLine($"Доступные места в порту: {port.get_avaliable_place()}");
                Console.Write($"----------------------------------------------------------------\n");
            }
            
        }

        static void OnTimerElapsed1(List<Ship> ships, List<Ship> waiter_ship, Port port, List<Ship> docked_ships, List<Ship> leave_ship)
        {
            ship_wait(port, ships, waiter_ship);

            foreach (var a in waiter_ship.ToArray()) 
            {
                ship_arrival(port, a, docked_ships, waiter_ship);
            }

            time += 10;

            foreach (var a in docked_ships.ToArray()) 
            {
                ship_departure(port, a, docked_ships, leave_ship);
            }
        }

        static void last_line(int num_line )
        {
            Console.SetCursorPosition(0, Console.CursorTop - num_line);
            for (int i = 0; i < num_line + 1; i++)
            {
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, Console.CursorTop - num_line);
        }
    }
}



