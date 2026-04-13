using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Moedelo.CommonV2.Utils
{
    [Obsolete("Этот класс содержит ошибку и будет удалён. Используйте UserUid из accounts-shared")]
    public static class UidGeneration
    {
        private const int UidFirstDigitMultiplier = 100000000;
        private const int UidSeed = 88002007;

        public static string GetUidFromId(int id, string strFormat = "### ### ###")
        {
            int xoring = id ^ UidSeed;
            int firstDigit = id.ToString(CultureInfo.InvariantCulture).Length;

            return (firstDigit * UidFirstDigitMultiplier + xoring).ToString(strFormat);
        }

        public static int GetUserIdFromUid(string uid)
        {
            if (string.IsNullOrEmpty(uid))
                return -1;
            int a = ExraxtIntFromUid(uid);
            if (a == -1)
                return -1;
            int result;
            Math.DivRem(a, UidFirstDigitMultiplier, out result);
            return result ^ UidSeed;
        }

        public static int ExraxtIntFromUid(string uid)
        {
            uid = Regex.Replace(uid, @"\D+", string.Empty);
            int digitUid;
            if (!int.TryParse(uid, out digitUid))
            {
                return -1;
            }

            return digitUid;
        }
    }
}