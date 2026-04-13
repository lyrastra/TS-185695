using System;
using System.Linq;

namespace Moedelo.Address.Shared
{
    public static class TypelessAddressObject
    {
        private static readonly string[] Map =
        {
            #region Region
            "Кемеровская область - Кузбасс"
            #endregion
        };

        public static bool Check(string address)
        {
            return Map.Any(a => address.StartsWith(a, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}