namespace Moedelo.Common.Utils.Numbers
{
    /// <summary>
    /// Приведение телефонных номеров к одому формату
    /// https://github.com/moedelo/md-commonV2/blob/master/src/utils/Moedelo.CommonV2.Utils/NumberFormatHelper.cs
    /// </summary>
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