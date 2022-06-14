using lab1.Models;
using System.Collections.Generic;

namespace lab1.Extensions
{
    internal static class Extensions
    {
        public static void AddValue(this Dictionary<Country, int> dict, Country key, int value)
        {
            if (dict.ContainsKey(key))
                dict[key] += value;
            else dict.Add(key, value);
        }

        public static void SetValue(this Dictionary<Country, int> dict, Country key, int value)
        {
            if (dict.ContainsKey(key))
                dict[key] = value;
            else dict.Add(key, value);
        }
    }
}
