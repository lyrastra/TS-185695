using System;

namespace Moedelo.Infrastructure.SqlDataAccess
{
    internal static class ArgumentExtensions
    {
        internal static void NotNullOrEmpty(string arg, string argName)
        {
            if (string.IsNullOrEmpty(arg))
            {
                throw new ArgumentNullException(argName, $"{argName} cannot be null");
            }
        }
        
        internal static void NotNull<T>(T arg, string argName)
            where T : class
        {
            if (arg == null)
            {
                throw new ArgumentNullException(argName, $"{argName} cannot be null");
            }
        }
    }
}