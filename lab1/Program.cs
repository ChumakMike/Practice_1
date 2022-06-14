using lab1.Helpers;
using lab1.Processes;
using lab1.Services;
using lab1.Validators;
using System;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            int cases = 0;
            var lines = Parser.ReadFile(Constants.Path);
            Console.WriteLine("");
            while (lines.Count > 0)
            {
                cases++;
                var mapService = new MapService(new CityService());

                var map = Parser.ReadMap(lines, mapService);

                var isCountriesAccessible = map != null && MapValidator.CheckIfCountriesAreAccessible(map);
                var process = new DiffusionProcess(mapService);

                if (isCountriesAccessible)
                {
                    Console.WriteLine("Case Number " + cases);
                    process.Diffuse(map);
                }
                else if (map != null)
                {
                    Console.WriteLine("Some countries are not accessible!");
                }
            }
            Console.ReadKey();
        }
    }
}
