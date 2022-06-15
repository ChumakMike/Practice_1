using lab1.Interfaces;
using lab1.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace lab1.Helpers
{
    public class Parser
    {
        public static List<string> ReadFile(string path)
        {
            using StreamReader file = new StreamReader(path);
            var result = new List<string>();
            while (file.ReadLine() is { } line)
            {
                result.Add(line);
                Console.WriteLine(line);
            }

            return result;
        }

        public static MapContainer ReadMap(List<string> lines, IMapService mapService)
        {
            var countriesCount = int.Parse(lines.ElementAt(0));
            lines.RemoveAt(0);

            if (countriesCount == 0)
            {
                Console.WriteLine("");
                return null;
            }

            var map = new MapContainer();

            for (int i = 0; i < countriesCount; i++)
            {
                var splits = lines.ElementAt(0).Split(" ");
                mapService.AddCountryWithCitiesOnMap(map, new Country(splits[0], int.Parse(splits[1]), int.Parse(splits[2]),
                        int.Parse(splits[3]), int.Parse(splits[4])));
                lines.RemoveAt(0);
            }
            return map;
        }
    }
}
