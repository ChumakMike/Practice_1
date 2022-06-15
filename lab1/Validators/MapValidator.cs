using lab1.Models;
using System.Collections.Generic;
using System.Linq;

namespace lab1.Validators
{
    internal static class MapValidator
    {
        public static bool CheckIfLeftNeighborCanExist(int[,] map, int x, int y)
        {
            return x != Constants.MinCoordinate - 1 && map[x - 1, y] != 0;
        }

        public static bool CheckIfRightNeighborCanExist(int[,] map, int x, int y)
        {
            return x != Constants.MaxCoordinate - 1 && map[x + 1, y] != 0;
        }

        public static bool CheckIfUpperNeighborCanExist(int[,] map, int x, int y)
        {
            return y != Constants.MaxCoordinate - 1 && map[x, y + 1] != 0;
        }

        public static bool CheckIfLowerNeighborCanExist(int[,] map, int x, int y)
        {
            return y != Constants.MinCoordinate - 1 && map[x, y - 1] != 0;
        }

        public static bool CheckIfCountriesAreAccessible(MapContainer container)
        {
            var currentCountryId = container.Countries.Keys.ToList().FirstOrDefault();
            var countriesOnMap = container.Countries.Count;

            List<int> accessibleCountriesIds = new List<int>();
            List<int> currentCountryNeighborsIds = new List<int>();

            while (accessibleCountriesIds.Count < countriesOnMap)
            {
                accessibleCountriesIds.Add(currentCountryId);
                AddNeighborsForCountry(container, currentCountryId, accessibleCountriesIds, currentCountryNeighborsIds);
                if (!currentCountryNeighborsIds.Any()) break;
                currentCountryId = currentCountryNeighborsIds.ElementAt(0);
            }
            return accessibleCountriesIds.Count == countriesOnMap;
        }

        private static void AddNeighborsForCountry(MapContainer container, int id,
            List<int> accessibleCountriesIds, List<int> currentCountryNeighborsIds)
        {
            var currentCountry = container.Countries[id];

            for (int i = currentCountry.Xl; i <= currentCountry.Xh - currentCountry.Xl; i++)
            {
                if (currentCountry.Yl != Constants.MinCoordinate)
                    AddCountryToNeighborsList(container.Map[currentCountry.Xl + i, currentCountry.Yl - 1], accessibleCountriesIds, currentCountryNeighborsIds);

                if (currentCountry.Yh != Constants.MaxCoordinate)
                    AddCountryToNeighborsList(container.Map[currentCountry.Xl + i, currentCountry.Yh + 1], accessibleCountriesIds, currentCountryNeighborsIds);
            }

            for (int i = 0; i <= currentCountry.Yh - currentCountry.Yl; i++)
            {
                if (currentCountry.Xl != Constants.MinCoordinate)
                    AddCountryToNeighborsList(container.Map[currentCountry.Xl - 1, currentCountry.Yl + i], accessibleCountriesIds, currentCountryNeighborsIds);

                if (currentCountry.Xh != Constants.MaxCoordinate)
                    AddCountryToNeighborsList(container.Map[currentCountry.Xh + 1, i + currentCountry.Yl + i], accessibleCountriesIds, currentCountryNeighborsIds);
            }
        }

        private static void AddCountryToNeighborsList(int id, List<int> accessibleCountriesIds, List<int> currentCountryNeighborsIds)
        {
            if (id != 0 && !(accessibleCountriesIds.Contains(id) || currentCountryNeighborsIds.Contains(id)))
                currentCountryNeighborsIds.Add(id);
        }
    }
}
