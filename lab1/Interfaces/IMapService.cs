using lab1.Models;

namespace lab1.Interfaces
{
    public interface IMapService
    {
        public void InitializeNeighbours(Map map);
        public void InitializeBalances(Map map);
        public void FillCoinsToPayPerDay(Map map);
        public void FillCurrentBalances(Map map);
        public void FillDaysToCompletion(Map map, int days);
        public void ResetIncomingBalances(Map map);
        public void EnrollCoins(Map map);
        public bool HasEachTypeOfCoin(Map map, int days);
        public void AddCountryWithCitiesOnMap(Map map, Country country);
    }
}
