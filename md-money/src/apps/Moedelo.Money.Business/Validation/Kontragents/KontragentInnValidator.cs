using Moedelo.Money.Business.Abstractions.Exceptions;
using System.Linq;
using System.Text.RegularExpressions;

namespace Moedelo.Money.Business.Validation.Kontragents
{
    internal static class KontragentInnValidator
    {
        private static readonly Regex Inn10 = new Regex(@"^\d{10}$", RegexOptions.Compiled);
        private static readonly Regex Inn12 = new Regex(@"^\d{12}$", RegexOptions.Compiled);
        private static readonly int[] Inn10f = new[] { 2, 4, 10, 3, 5, 9, 4, 6, 8, 0 }.Reverse().ToArray();
        private static readonly int[] Inn12f11 = new[] { 7, 2, 4, 10, 3, 5, 9, 4, 6, 8, 0 }.Reverse().ToArray();
        private static readonly int[] Inn12f12 = new[] { 3, 7, 2, 4, 10, 3, 5, 9, 4, 6, 8, 0 }.Reverse().ToArray();

        public static void Validate10(string inn)
        {
            if (string.IsNullOrEmpty(inn))
            {
                return;
            }

            if (!Inn10.IsMatch(inn))
            {
                throw new BusinessValidationException("Contractor.Inn", "ИНН должен состоять из 10 цифр");
            }

            if (IsValidInn10(inn) == false)
            {
                throw new BusinessValidationException("Contractor.Inn", "Неправильное контрольное число");
            }
        }

        public static void Validate12(string inn)
        {
            if (string.IsNullOrEmpty(inn))
            {
                return;
            }

            if (!Inn12.IsMatch(inn))
            {
                throw new BusinessValidationException("Contractor.Inn", "ИНН должен состоять из 12 цифр");
            }

            if (IsValidInn12(inn) == false)
            {
                throw new BusinessValidationException("Contractor.Inn", "Неправильное контрольное число");
            }
        }

        /// <summary>
        /// Алгоритм проверки ИНН 10 знаков:
        /// 1. Вычисляется контрольная сумма со следующими весовыми коэффициентами: (2,4,10,3,5,9,4,6,8,0)
        /// 2. Вычисляется контрольное число как остаток от деления контрольной суммы на 11
        /// 3. Если контрольное число больше 9, то контрольное число вычисляется как остаток от деления контрольного числа на 10
        /// 4. Контрольное число проверяется с десятым знаком ИНН. В случае их равенства ИНН считается правильным.
        /// </summary>
        /// <param name="textValue"></param>
        /// <returns></returns>
        private static bool IsValidInn10(string textValue)
        {
            var value = long.Parse(textValue);

            // 1.Вычисляется контрольная сумма со следующими весовыми коэффициентами: (2,4,10,3,5,9,4,6,8,0)
            var sum = CalculateCheckSum(value, Inn10f);

            // 2.Вычисляется контрольное число как остаток от деления контрольной суммы на 11
            // 3.Если контрольное число больше 9, то контрольное число вычисляется как остаток от деления контрольного числа на 10
            var k1 = sum % 11 % 10;

            // 4.Контрольное число проверяется с десятым знаком ИНН. В случае их равенства ИНН считается правильным
            return k1 == value % 10;
        }

        /// <summary>
        /// Алгоритм проверки ИНН 12 знаков.
        /// 1. Вычисляется контрольная сумма по 11-ти знакам со следующими весовыми коэффициентами: (7,2,4,10,3,5,9,4,6,8,0)
        /// 2. Вычисляется контрольное число(1) как остаток от деления контрольной суммы на 11
        /// 3. Если контрольное число(1) больше 9, то контрольное число(1) вычисляется как остаток от деления контрольного числа(1) на 10
        /// 4. Вычисляется контрольная сумма по 12-ти знакам со следующими весовыми коэффициентами: (3,7,2,4,10,3,5,9,4,6,8,0).
        /// 5. Вычисляется контрольное число(2) как остаток от деления контрольной суммы на 11
        /// 6. Если контрольное число(2) больше 9, то контрольное число(2) вычисляется как остаток от деления контрольного числа(2) на 10
        /// 7. Контрольное число(1) проверяется с одиннадцатым знаком ИНН и контрольное число(2) проверяется с двенадцатым знаком ИНН.
        /// В случае их равенства ИНН считается правильным.
        /// </summary>
        /// <param name="textValue"></param>
        /// <returns></returns>
        private static bool IsValidInn12(string textValue)
        {
            var value = long.Parse(textValue);

            // 1. Вычисляется контрольная сумма по 11 - ти знакам со следующими весовыми коэффициентами: (7,2,4,10,3,5,9,4,6,8,0)
            var sum11 = CalculateCheckSum(value / 10, Inn12f11);
            // 2. Вычисляется контрольное число(1) как остаток от деления контрольной суммы на 11
            // 3. Если контрольное число(1) больше 9, то контрольное число(1) вычисляется как остаток от деления контрольного числа(1) на 10
            var k11 = sum11 % 11 % 10;

            // 7.1. Контрольное число(1) проверяется с одиннадцатым знаком ИНН
            if (k11 != (value / 10) % 10)
            {
                return false; // неверное значение 11 символа
            }

            // 4.Вычисляется контрольная сумма по 12 - ти знакам со следующими весовыми коэффициентами: (3,7,2,4,10,3,5,9,4,6,8,0).
            var sum12 = CalculateCheckSum(value, Inn12f12);
            // 5. Вычисляется контрольное число(2) как остаток от деления контрольной суммы на 11
            // 6. Если контрольное число(2) больше 9, то контрольное число(2) вычисляется как остаток от деления контрольного числа(2) на 10
            var k12 = sum12 % 11 % 10;

            // 7.2. контрольное число(2) проверяется с двенадцатым знаком ИНН.
            return k12 == value % 10;
        }

        private static long CalculateCheckSum(long value, int[] fList)
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
