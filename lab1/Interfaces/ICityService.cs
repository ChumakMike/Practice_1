using lab1.Models;
using System.Collections.Generic;

namespace lab1.Interfaces
{
    public interface ICityService
    {
        public void InitializeBalances(City city, List<Country> countries);
        public bool HasEachTypeOfCoin(City city, List<Country> countries);
        public void EnrollCoins(City city, City neighbour, Country country);
        public void FillCurrentBalance(City city);
        public void FillCoinsToPayPerDay(City city);
        public void ResetIncomingBalance(City city);
    }
}
