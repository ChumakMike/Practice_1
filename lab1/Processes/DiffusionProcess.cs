using lab1.Interfaces;
using lab1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace lab1.Processes
{
    public class DiffusionProcess : IDiffusionProcess
    {
        private readonly IMapService _mapService;

        public DiffusionProcess(IMapService mapService)
        {
            _mapService = mapService;
        }


        public void Diffuse(MapContainer map)
        {
            BeforeExecution(map);
            Execute(map);
            AfterExecution(map);
        }

        private void BeforeExecution(MapContainer map)
        {
            _mapService.InitializeNeighbours(map);
            _mapService.InitializeBalances(map);
        }

        private void AfterExecution(MapContainer map)
        {
            var countriesToOutput = new List<Country>(map.Countries.Values.ToList().OrderBy(x => x.DaysToCompletion));
            foreach (var country in countriesToOutput)
                Console.WriteLine(country.Name + " " + country.DaysToCompletion);
        }

        private void Execute(MapContainer map)
        {
            int days = 0;

            do
            {
                _mapService.FillCoinsToPayPerDay(map);
                _mapService.EnrollCoins(map);
                _mapService.FillCurrentBalances(map);
                _mapService.ResetIncomingBalances(map);
                days++;
                _mapService.FillDaysToCompletion(map, days);
            } while (!_mapService.HasEachTypeOfCoin(map, days));
        }
    }
}
