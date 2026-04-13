using System.Text.RegularExpressions;

namespace Moedelo.HomeV2.Client.Phone.Helper
{
    public class PhoneHelper
    {
        private const int MaxPhoneLength = 10;

        private static readonly Regex NonDigit = new Regex(@"\D", RegexOptions.Compiled);

        public static string TrimPhoneNumberLengthToTen(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return null;
            }

            string temp = NonDigit.Replace(number, string.Empty);

            if (temp.Length > MaxPhoneLength)
            {
                return temp.Substring(temp.Length - MaxPhoneLength, MaxPhoneLength);
            }

            return temp;
        }

        public static string Format(string number, string formatCode = "+7")
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return null;
            }

            var trimmedPhone = TrimPhoneNumberLengthToTen(number);
            if (trimmedPhone.Length == 10)
            {
                return $"{formatCode}{trimmedPhone}";
            }
            return trimmedPhone;
        }
    }
}