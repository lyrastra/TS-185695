using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class OkpoAttribute : ValidationAttribute
    {
        private static readonly Regex OnlyDigits = new Regex(@"^\d{4,10}$", RegexOptions.Compiled);
        private static readonly int[] Weights8_1 = { 1, 2, 3, 4, 5, 6, 7 };
        private static readonly int[] Weights8_2 = { 3, 4, 5, 6, 7, 8, 9 };
        private static readonly int[] Weights10_1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private static readonly int[] Weights10_2 = { 3, 4, 5, 6, 7, 8, 9, 10, 11 };

        public OkpoAttribute() : base("Неверный формат ОКПО") { }

        public override bool IsValid(object value)
        {
            var okpoStr = value?.ToString();
            
            if (string.IsNullOrEmpty(okpoStr)) 
                return true;
            if (!OnlyDigits.IsMatch(okpoStr)) 
                return false;

            // Приведение к 8 или 10 символам при необходимости
            if (okpoStr.Length == 7 || okpoStr.Length == 9)
                okpoStr = okpoStr.PadLeft(okpoStr.Length + 1, '0');

            if (okpoStr.Length < 8 && okpoStr.Length > 10) 
                return false;
            
            if (okpoStr.Length == 8)
                return CheckOkpo(okpoStr, Weights8_1, Weights8_2);
            if (okpoStr.Length == 10)
                return CheckOkpo(okpoStr, Weights10_1, Weights10_2);

            return false;
        }

        /// <summary>
        ///     Методика расчёта контрольного числа для кода ОКПО
        ///     Контрольное число рассчитывается следующим образом:
        ///     1. Контрольной цифрой кода является последняя цифра - восьмая в восьмизначном коде и десятая в десятизначном.
        ///     2. Разрядам кода в общероссийском классификаторе, начиная со старшего разряда, присваивается набор весов,
        ///     соответствующий натуральному ряду чисел от 1 до 10. Если разрядность кода больше 10, то набор весов повторяется.
        ///     3. Каждая цифра кода, кроме последней, умножается на вес разряда и вычисляется сумма полученных произведений.
        ///     4. Контрольное число для кода представляет собой остаток от деления полученной суммы на модуль «11».
        ///     5. Контрольное число должно иметь один разряд, значение которого находится в пределах от 0 до 9.
        ///     6. Если получается остаток, равный 10, то для обеспечения одноразрядного контрольного числа необходимо провести
        ///     повторный расчет, применяя вторую последовательность весов, сдвинутую на два разряда влево (3, 4, 5,…). Если в
        ///     случае повторного расчета остаток от деления вновь сохраняется равным 10, то значение контрольного числа
        ///     проставляется равным «0»
        ///     https://ru.wikipedia.org/wiki/%D0%9A%D0%BE%D0%BD%D1%82%D1%80%D0%BE%D0%BB%D1%8C%D0%BD%D0%BE%D0%B5_%D1%87%D0%B8%D1%81%D0%BB%D0%BE#.D0.9D.D0.BE.D0.BC.D0.B5.D1.80_.D0.9E.D0.9A.D0.9F.D0.9E
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool CheckOkpo(string value, int[] w1, int[] w2)
        {
            var digits = value.Select(c => c - '0').ToArray();
            var ctrl = digits[digits.Length - 1];
            var cv = Mod11(digits, w1);

            if (cv == 10)
                cv = Mod11(digits, w2);
            if (cv == 10)
                cv = 0;

            return ctrl == cv;
        }

        private static int Mod11(int[] digits, int[] weights)
        {
            if (digits.Length != weights.Length + 1)
                return 0;
            var sum = weights.Select((t, i) => digits[i] * t).Sum();
            return sum % 11;
        }
    }
}