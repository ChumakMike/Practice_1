using lab1.Interfaces;
using lab1.Models;
using lab1.Validators;
using System.Collections.Generic;
using System.Linq;

namespace lab1.Services
{
    public class MapService : IMapService
    {
        private readonly ICityService _cityService;

        public MapService(ICityService cityService)
        {
            _cityService = cityService;
        }
        public void InitializeNeighbours(Map map)
        {
            foreach (var city in map.Cities)
            {
                int x = city.Value.X;
                int y = city.Value.Y;

                var neighborsList = new List<City>();

                if (MapValidator.CheckIfUpperNeighborCanExist(map._map, x, y))
                    neighborsList.Add(map.Cities[GetUniqueCityCode(x, y + 1)]);
                if (MapValidator.CheckIfLowerNeighborCanExist(map._map, x, y))
                    neighborsList.Add(map.Cities[GetUniqueCityCode(x, y - 1)]);
                if (MapValidator.CheckIfLeftNeighborCanExist(map._map, x, y))
                    neighborsList.Add(map.Cities[GetUniqueCityCode(x - 1, y)]);
                if (MapValidator.CheckIfRightNeighborCanExist(map._map, x, y))
                    neighborsList.Add(map.Cities[GetUniqueCityCode(x + 1, y)]);

                map.CityToNeighbours.Add(city.Value, neighborsList);
            }
        }

        public void InitializeBalances(Map map)
        {
            foreach (var city in map.Cities)
                _cityService.InitializeBalances(city.Value, map.Countries.Values.ToList());
        }

        public void FillCoinsToPayPerDay(Map map)
        {
            foreach (var city in map.Cities)
                _cityService.FillCoinsToPayPerDay(city.Value);
        }

        public void FillCurrentBalances(Map map)
        {
            foreach (var city in map.Cities)
                _cityService.FillCurrentBalance(city.Value);
        }

        public void FillDaysToCompletion(Map map, int days)
        {
            if (map.Countries.Count == 1)
                map.Countries.First().Value.DaysToCompletion = 0;
            foreach (var country in map.Countries)
                if (country.Value.DaysToCompletion < 0)
                    country.Value.DaysToCompletion = days;
            foreach (var city in map.Cities)
                if (!_cityService.HasEachTypeOfCoin(city.Value, map.Countries.Values.ToList()))
                    city.Value.Country.DaysToCompletion = -1;
        }

        public void ResetIncomingBalances(Map map)
        {
            foreach (var city in map.Cities)
                _cityService.ResetIncomingBalance(city.Value);
        }

        public void EnrollCoins(Map map)
        {
            var cities = map.CityToNeighbours.Keys.ToList();
            var countries = map.Countries.Values.ToList();
            foreach (var city in cities)
                foreach (var neighborCity in map.CityToNeighbours[city])
                    foreach (var country in countries)
                        _cityService.EnrollCoins(city, neighborCity, country);
        }

        public bool HasEachTypeOfCoin(Map map, int days)
        {
            foreach (var city in map.Cities)
                if (!_cityService.HasEachTypeOfCoin(city.Value, map.Countries.Values.ToList()))
                    return false;
            return true;
        }

        public void AddCountryWithCitiesOnMap(Map map, Country country)
        {
            map.Countries.Add(map.Countries.Count + 1, country);
            AddCitiesOfCountryOnMap(map, country);
        }

        private void AddCitiesOfCountryOnMap(Map map, Country country)
        {
            for (int y = country.Yl; y <= country.Yh; y++)
            {
                for (int x = country.Xl; x <= country.Xh; x++)
                {
                    var city = new City(x, y, country);
                    if (!map.Cities.ContainsKey(GetUniqueCityCode(city.X, city.Y)))
                        map.Cities.Add(GetUniqueCityCode(city.X, city.Y), city);
                    map._map[x, y] = map.Countries.Count;
                }
            }
        }

        private int GetUniqueCityCode(int x, int y) => Constants.MaxCoordinate * x + y;
    }
}
