using System.Collections.Generic;

namespace lab1.Models
{
    public class City
    {
        public Country Country { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public City(int x, int y, Country country)
        {
            X = x;
            Y = y;
            Country = country;
        }

        public Dictionary<Country, int> CurrentBalance = new Dictionary<Country, int>();

        public Dictionary<Country, int> IncomingBalance = new Dictionary<Country, int>();

        public Dictionary<Country, int> CoinsToPayPerDay = new Dictionary<Country, int>();
    }
}
