using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Moedelo.Infrastructure.AspNetCore.Validation
{
    public class OkpoAttribute : ValidationAttribute
    {
        private const long MinOkpoValue = 1000; // окпо не может содержать менее 4 символов
        private const long МaxOkpo8 = 99999999; // максимальное значение 8-разрядного ОКПО
        private static readonly Regex OnlyDigits = new Regex(@"^\d{4,10}$");
        private static readonly int[] Okpo8f1 = {7, 6, 5, 4, 3, 2, 1};
        private static readonly int[] Okpo8f2 = {9, 8, 7, 6, 5, 4, 3};
        private static readonly int[] Okpo10f1 = {9, 8, 7, 6, 5, 4, 3, 2, 1};
        private static readonly int[] Okpo10f2 = {1, 10, 9, 8, 7, 6, 5, 4, 3};

        public OkpoAttribute()
            : base("Неверный формат ОКПО")
        {
        }

        public override bool IsValid(object obj)
        {
            var textValue = obj?.ToString();

            if (string.IsNullOrEmpty(textValue))
            {
                return true;
            }

            // проверка длины и отсутствия не цифр
            if (!OnlyDigits.IsMatch(textValue))
            {
                return false;
            }

            var value = long.Parse(textValue);

            if (value < MinOkpoValue)
            {
                return false;
            }

            return value <= МaxOkpo8
                ? IsOkpo8(value)
                : IsOkpo10(value);
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
        private static bool IsOkpo8(long value)
        {
            if (value > МaxOkpo8)
            {
                return false;
            }

            var cv = value % 10;
            var digits = value / 10;

            return cv == CalculateCv(digits, Okpo8f1, Okpo8f2);
        }

        /// <summary>
        /// см. алгоритм в IsOkpo8
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsOkpo10(long value)
        {
            var cv = value % 10;
            var digits = value / 10;

            return cv == CalculateCv(digits, Okpo10f1, Okpo10f2);
        }

        private static long CalculateCv(long digits, int[] weights1, int[] weights2)
        {
            var cv = CalculateCvOverWeights(digits, weights1) % 11;
            return cv == 10 ? CalculateCvOverWeights(digits, weights2) % 11 % 10 : cv;
        }


        private static long CalculateCvOverWeights(long value, int[] fList)
        {
            var v = value;
            long sum = 0;
            foreach (var f in fList)
            {
                var c = v % 10;
                v /= 10;
                sum += c * f;
            }

            return sum;
        }
    }
}