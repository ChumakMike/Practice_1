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
        public void InitializeNeighbours(MapContainer container)
        {
            foreach (var city in container.Cities)
            {
                int x = city.Value.X;
                int y = city.Value.Y;

                var neighborsList = new List<City>();

                if (MapValidator.CheckIfUpperNeighborCanExist(container.Map, x, y))
                    neighborsList.Add(container.Cities[GetUniqueCityCode(x, y + 1)]);
                if (MapValidator.CheckIfLowerNeighborCanExist(container.Map, x, y))
                    neighborsList.Add(container.Cities[GetUniqueCityCode(x, y - 1)]);
                if (MapValidator.CheckIfLeftNeighborCanExist(container.Map, x, y))
                    neighborsList.Add(container.Cities[GetUniqueCityCode(x - 1, y)]);
                if (MapValidator.CheckIfRightNeighborCanExist(container.Map, x, y))
                    neighborsList.Add(container.Cities[GetUniqueCityCode(x + 1, y)]);

                container.CityToNeighbours.Add(city.Value, neighborsList);
            }
        }

        public void InitializeBalances(MapContainer container)
        {
            foreach (var city in container.Cities)
                _cityService.InitializeBalances(city.Value, container.Countries.Values.ToList());
        }

        public void FillCoinsToPayPerDay(MapContainer container)
        {
            foreach (var city in container.Cities)
                _cityService.FillCoinsToPayPerDay(city.Value);
        }

        public void FillCurrentBalances(MapContainer container)
        {
            foreach (var city in container.Cities)
                _cityService.FillCurrentBalance(city.Value);
        }

        public void FillDaysToCompletion(MapContainer container, int days)
        {
            if (container.Countries.Count == 1)
                container.Countries.First().Value.DaysToCompletion = 0;
            foreach (var country in container.Countries)
                if (country.Value.DaysToCompletion < 0)
                    country.Value.DaysToCompletion = days;
            foreach (var city in container.Cities)
                if (!_cityService.HasEachTypeOfCoin(city.Value, container.Countries.Values.ToList()))
                    city.Value.Country.DaysToCompletion = -1;
        }

        public void ResetIncomingBalances(MapContainer container)
        {
            foreach (var city in container.Cities)
                _cityService.ResetIncomingBalance(city.Value);
        }

        public void EnrollCoins(MapContainer container)
        {
            var cities = container.CityToNeighbours.Keys.ToList();
            var countries = container.Countries.Values.ToList();
            foreach (var city in cities)
                foreach (var neighborCity in container.CityToNeighbours[city])
                    foreach (var country in countries)
                        _cityService.EnrollCoins(city, neighborCity, country);
        }

        public bool HasEachTypeOfCoin(MapContainer container, int days)
        {
            foreach (var city in container.Cities)
                if (!_cityService.HasEachTypeOfCoin(city.Value, container.Countries.Values.ToList()))
                    return false;
            return true;
        }

        public void AddCountryWithCitiesOnMap(MapContainer container, Country country)
        {
            container.Countries.Add(container.Countries.Count + 1, country);
            AddCitiesOfCountryOnMap(container, country);
        }

        private void AddCitiesOfCountryOnMap(MapContainer container, Country country)
        {
            for (int y = country.Yl; y <= country.Yh; y++)
            {
                for (int x = country.Xl; x <= country.Xh; x++)
                {
                    var city = new City(x, y, country);
                    if (!container.Cities.ContainsKey(GetUniqueCityCode(city.X, city.Y)))
                        container.Cities.Add(GetUniqueCityCode(city.X, city.Y), city);
                    container.Map[x, y] = container.Countries.Count;
                }
            }
        }

        private int GetUniqueCityCode(int x, int y) => Constants.MaxCoordinate * x + y;
    }
}
