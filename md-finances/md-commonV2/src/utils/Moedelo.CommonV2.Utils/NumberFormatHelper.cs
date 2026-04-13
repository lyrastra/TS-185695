namespace Moedelo.CommonV2.Utils
{
    public static class NumberFormatHelper
    {
        public static string GetFormatedNumber(string number)
        {
            var result = number.Replace("(", string.Empty).Replace(")", string.Empty).Replace("-", string.Empty).Replace(" ", string.Empty);

            if (result[0] == '+')
            {
                result = result.Substring(1);
            }

            if (result[0] == '8' || result[0] == '7')
            {
                result = result.Substring(1);
            }

            return $"+7{result}";
        }
    }
}