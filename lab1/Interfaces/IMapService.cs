using lab1.Models;

namespace lab1.Interfaces
{
    public interface IMapService
    {
        public void InitializeNeighbours(MapContainer container);
        public void InitializeBalances(MapContainer container);
        public void FillCoinsToPayPerDay(MapContainer container);
        public void FillCurrentBalances(MapContainer container);
        public void FillDaysToCompletion(MapContainer container, int days);
        public void ResetIncomingBalances(MapContainer container);
        public void EnrollCoins(MapContainer container);
        public bool HasEachTypeOfCoin(MapContainer container, int days);
        public void AddCountryWithCitiesOnMap(MapContainer container, Country country);
    }
}
