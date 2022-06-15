using System;

namespace lab1.Validators
{
    internal static class CountryValidator
    {
        public static void ValidateBeforeInitialize(string name, int xl, int yl, int xh, int yh)
        {
            ValidateCountryName(name);
            ValidateLowerAndHigherCoordinate(xl, xh);
            ValidateLowerAndHigherCoordinate(yl, yh);
        }
        private static void ValidateLowerAndHigherCoordinate(int lower, int higher)
        {
            if (lower < Constants.MinCoordinate || lower > Constants.MaxCoordinate)
                throw new ArgumentOutOfRangeException(nameof(lower));
            if (higher < lower || higher > Constants.MaxCoordinate)
                throw new ArgumentOutOfRangeException(nameof(higher));
        }

        private static void ValidateCountryName(string name)
        {
            if (name?.Length > Constants.MaxNameLength)
                throw new ArgumentException("Country name must be less than 25 characters!");
        }
    }
}
