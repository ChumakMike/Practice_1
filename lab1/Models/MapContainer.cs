using System.Collections.Generic;

namespace lab1.Models
{
    public class MapContainer
    {
        public int[,] Map { get; }
        public Dictionary<int, City> Cities { get; }
        public Dictionary<int, Country> Countries { get; }
        public Dictionary<City, List<City>> CityToNeighbours { get; }

        public MapContainer()
        {
            Map = new int[Constants.MaxCoordinate, Constants.MaxCoordinate];
            Cities = new Dictionary<int, City>();
            Countries = new Dictionary<int, Country>();
            CityToNeighbours = new Dictionary<City, List<City>>();
        }
    }
}
