using lab1.Extensions;
using lab1.Interfaces;
using lab1.Models;
using System.Collections.Generic;
using System.Linq;

namespace lab1.Services
{
    internal class CityService : ICityService
    {
        public void InitializeBalances(City city, List<Country> countries)
        {
            foreach (var country in countries)
            {
                city.CurrentBalance.Add(country, city.Country.Equals(country) ? Constants.InitialCoins : 0);
                city.IncomingBalance.Add(country, 0);
                city.CoinsToPayPerDay.Add(country, 0);
            }
        }

        public bool HasEachTypeOfCoin(City city, List<Country> countries)
        {
            foreach (var country in countries)
            {
                if (city.CurrentBalance[country] == 0)
                    return false;
            }
            return true;
        }

        public void EnrollCoins(City city, City neighbour, Country country)
        {
            var toPayCoins = city.CoinsToPayPerDay[country];
            city.CurrentBalance[country] -= toPayCoins;
            neighbour.IncomingBalance.AddValue(country, toPayCoins);
        }

        public void FillCurrentBalance(City city)
        {
            var keys = city.CurrentBalance.Keys.ToList();
            foreach (var key in keys)
                city.CurrentBalance.AddValue(key, city.IncomingBalance[key]);
        }

        public void FillCoinsToPayPerDay(City city)
        {
            foreach (var item in city.CurrentBalance)
                city.CoinsToPayPerDay[item.Key] = city.CurrentBalance[item.Key] / 1000;
        }

        public void ResetIncomingBalance(City city)
        {
            var keys = city.IncomingBalance.Keys.ToList();
            foreach (var key in keys)
                city.IncomingBalance.SetValue(key, 0);
        }
    }
}
