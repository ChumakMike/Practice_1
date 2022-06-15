using lab1.Validators;

namespace lab1.Models
{
    public class Country
    {
        public string Name { get; set; }
        public int Xl { get; set; }
        public int Yl { get; set; }
        public int Xh { get; set; }
        public int Yh { get; set; }
        public int DaysToCompletion { get; set; }
        public Country(string name, int xl, int yl, int xh, int yh)
        {
            CountryValidator.ValidateBeforeInitialize(name, xl, yl, xh, yh);

            Name = name;
            Xl = xl;
            Xh = xh;
            Yh = yh;
            Yl = yl;
            DaysToCompletion = -1;
        }

    }
}
