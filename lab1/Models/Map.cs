using System.Collections.Generic;

namespace lab1.Models
{
    public class Map
    {
        public int[,] _map = new int[Constants.MaxCoordinate, Constants.MaxCoordinate];

        public Map()
        {

        }

        public Dictionary<int, City> Cities = new Dictionary<int, City>();

        public Dictionary<int, Country> Countries = new Dictionary<int, Country>();

        public Dictionary<City, List<City>> CityToNeighbours = new Dictionary<City, List<City>>();
    }
}
