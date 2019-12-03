using System;
using System.IO;

namespace devkutt
{
    class Program
    {
        static void Main(string[] args)
        {
            int totalFuel = 0;
            FileStream fileStream = new FileStream("input.txt", FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                var line = reader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    var mass = int.Parse(line);
                    var requiredFuel = CalculateFuelRecursive(mass);
                    Console.WriteLine($"Mass: {mass}, Fuel: {requiredFuel}");
                    totalFuel += requiredFuel;
                    line = reader.ReadLine();
                }
            }
            Console.WriteLine(totalFuel);
        }

        static int CalculateFuelRecursive(int mass, int totalFuel = 0)
        {
            int fuel = CalculateFuel(mass);
            if (fuel <= 0)
            {
                return totalFuel;
            }
            return CalculateFuelRecursive(fuel, totalFuel + fuel);
        }

        static int CalculateFuel(int mass)
        {
            return mass / 3 - 2;
        }
    }
}
