using System;
using System.Globalization;
using System.Threading;

namespace Moedelo.CommonV2.Models
{
    public struct Quarter
    {
        private static readonly DateTimeFormatInfo DateTimeFormatInfo =
            Thread.CurrentThread.CurrentCulture.DateTimeFormat;

        public Quarter(DateTime date)
        {
            Year = date.Year;
            Number = 0;

            if (date.Month >= 1 && date.Month <= 3)
            {
                Number = 1;
            }
            else if (date.Month >= 3 && date.Month <= 6)
            {
                Number = 2;
            }
            else if (date.Month >= 6 && date.Month <= 9)
            {
                Number = 3;
            }
            else if (date.Month >= 9 && date.Month <= 12)
            {
                Number = 4;
            }
        }

        public Quarter(int year, int number)
        {
            this.Year = year;
            this.Number = number;
        }

        public Quarter(QuarterParam quarterParam)
            : this(quarterParam.Year, quarterParam.Number)
        {
        }

        public int[] Monthes
        {
            get
            {
                switch (Number)
                {
                    case 1:
                        return new[] {1, 2, 3};
                    case 2:
                        return new[] {4, 5, 6};
                    case 3:
                        return new[] {7, 8, 9};
                    case 4:
                        return new[] {10, 11, 12};
                    default:
                        throw new InvalidOperationException("Недопустимое значение свойства Number: " + Number);
                }
            }
        }

        public QuarterType Type
        {
            get
            {
                switch (Number)
                {
                    case 1:
                        return QuarterType.First;
                    case 2:
                        return QuarterType.Second;
                    case 3:
                        return QuarterType.Third;
                    case 4:
                        return QuarterType.Fourth;
                    default:
                        throw new InvalidOperationException("Недопустимое значение свойства Number: " + Number);
                }
            }
        }

        public string PeriodCode
        {
            get
            {
                switch (Number)
                {
                    case 1:
                        return "03";
                    case 2:
                        return "06";
                    case 3:
                        return "09";
                    case 4:
                        return "12";
                    default:
                        throw new InvalidOperationException("Недопустимое значение свойства Number: " + Number);
                }
            }
        }

        /// <summary>
        /// Первый день квартала
        /// </summary>
        public DateTime FirstDate => new DateTime(Year, FirstMonthNumber, 1);

        /// <summary>
        /// Последний день квартала
        /// </summary>
        public DateTime LastDate => new DateTime(Year, ThirdMonthNumber, DateTime.DaysInMonth(Year, ThirdMonthNumber));

        public int Number { get; }

        public string RomeNumber
        {
            get
            {
                switch (Number)
                {
                    case 1:
                        return "I";
                    case 2:
                        return "II";
                    case 3:
                        return "III";
                    case 4:
                        return "IV";
                    default:
                        throw new InvalidOperationException("Недопустимое значение свойства Number: " + Number);
                }
            }
        }

        public int Year { get; }

        public int FirstMonthNumber => GetMonthNumberByType(QuarterMonthType.First);

        public int SecondMonthNumber => GetMonthNumberByType(QuarterMonthType.Second);

        public int ThirdMonthNumber => GetMonthNumberByType(QuarterMonthType.Third);

        public Quarter Next()
        {
            var nextNumber = Number < 4 ? Number + 1 : 1;
            var nextYear = Number < 4 ? Year : Year + 1;

            return new Quarter(nextYear, nextNumber);
        }

        public Quarter Previous()
        {
            var prevNumber = Number == 1 ? 4 : Number - 1;
            var prevYear = Number == 1 ? Year - 1 : Year;

            return new Quarter(prevYear, prevNumber);
        }

        public string GetMonthNameByType(QuarterMonthType monthType)
        {
            var monthNumber = GetMonthNumberByType(monthType);
            var monthName = DateTimeFormatInfo.GetMonthName(monthNumber);

            return monthName;
        }

        public int GetMonthNumberByType(QuarterMonthType monthType)
        {
            switch (monthType)
            {
                case QuarterMonthType.First:
                    return Monthes[0];
                case QuarterMonthType.Second:
                    return Monthes[1];
                case QuarterMonthType.Third:
                    return Monthes[2];
                default:
                    throw new ArgumentOutOfRangeException(nameof(monthType), monthType, "Недопустимое значение");
            }
        }
    }
}