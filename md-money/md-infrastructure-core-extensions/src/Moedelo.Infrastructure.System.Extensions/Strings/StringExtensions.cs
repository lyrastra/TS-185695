using System;
using System.Text;

namespace Moedelo.Infrastructure.System.Extensions.Strings
{
    public static class StringExtensions
    {
        public static string FromBase64(this string input, Encoding encoding)
        {
            return string.IsNullOrEmpty(input)
                ? string.Empty
                : encoding.GetString(Convert.FromBase64String(input));
        }
    }
}