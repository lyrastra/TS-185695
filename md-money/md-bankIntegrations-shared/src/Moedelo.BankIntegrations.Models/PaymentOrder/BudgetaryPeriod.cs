using System;
using Moedelo.BankIntegrations.Enums;

namespace Moedelo.BankIntegrations.Models.PaymentOrder
{
    public class BudgetaryPeriod
    {
        public BudgetaryPeriod()
        {
        }

        public BudgetaryPeriod(int number, BudgetaryPeriodType type, int year, DateTime? date = null)
        {
            Type = type;
            Number = number;
            Year = year;
            Date = date;
        }
        public DateTime? Date { get; set; }

        /// <summary> Тип периода: ГД, ПЛ, КВ, МС </summary>
        public BudgetaryPeriodType Type { get; set; }

        /// <summary>
        /// Номер в периоде: для МС — номер месяца, КВ — номер кваратала, ПЛ – номер полугодия, для ГД — 0.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Год платежа
        /// </summary>
        public int Year { get; set; }

        public override string ToString()
        {
            if (Type == BudgetaryPeriodType.Date && Date.HasValue)
            {
                return Date.Value.ToShortDateString();
            }

            return Type != BudgetaryPeriodType.None && Type != BudgetaryPeriodType.NoPeriod
                ? $"{Type.GetAbbreviation()}.{Number.ToString("00")}.{Year}"
                : "0";
        }

        /// <summary> распарсить тип периода платежа по строковому значения. </summary>
        /// <param name="type">Тип периода платежа: ГД, МС, ПЛ, КВ и т.п.</param>
        public static BudgetaryPeriodType ParsePaymentPeriodType(string type)
        {
            switch (SafeValue(type).ToLower())
            {
                case "гд":
                    return BudgetaryPeriodType.Year;
                case "пл":
                    return BudgetaryPeriodType.HalfYear;
                case "кв":
                    return BudgetaryPeriodType.Quarter;
                case "мс":
                    return BudgetaryPeriodType.Month;
                case "дт":
                    return BudgetaryPeriodType.Date;
                default:
                    return BudgetaryPeriodType.None;
            }
        }

        private static string SafeValue(string tempStr)
        {
            return string.IsNullOrEmpty(tempStr) ? string.Empty : tempStr;
        }

        public string PeriodToString()
        {
            switch (Type)
            {
                case BudgetaryPeriodType.Quarter:
                    return $"{Number} квартал {Year} г";

                case BudgetaryPeriodType.Month:
                    return new DateTime(Year, Number, 1).ToString("MMMM yyyy г");

                case BudgetaryPeriodType.HalfYear:
                    return $"{Number}-е полугодие {Year} г";

                case BudgetaryPeriodType.Decade1:
                    return new DateTime(Year, Number, 1).ToString("1-ю декаду MMMM yyyy г");

                case BudgetaryPeriodType.Decade2:
                    return new DateTime(Year, Number, 1).ToString("2-ю декаду MMMM yyyy г");

                case BudgetaryPeriodType.Decade3:
                    return new DateTime(Year, Number, 1).ToString("3-ю декаду MMMM yyyy г");

                case BudgetaryPeriodType.Date:
                    return Date?.ToShortDateString() ?? string.Empty;

                default:
                    return $"{Year} г";
            }
        }

        /// <summary>
        /// Последний день периода
        /// Для всех, кроме Декады
        /// </summary>
        public DateTime LastDayInPeriod
        {
            get
            {
                try
                {
                    switch (Type)
                    {
                        case BudgetaryPeriodType.Year:
                            return new DateTime(Year, 12, 31);

                        case BudgetaryPeriodType.Quarter:
                            return new DateTime(Year, Number * 3, DateTime.DaysInMonth(Year, Number * 3));

                        case BudgetaryPeriodType.Month:
                            return new DateTime(Year, Number, DateTime.DaysInMonth(Year, Number));

                        case BudgetaryPeriodType.HalfYear:
                            return new DateTime(Year, Number * 6, DateTime.DaysInMonth(Year, Number * 6));

                        case BudgetaryPeriodType.Decade1:
                            return new DateTime(Year, Number, 10);

                        case BudgetaryPeriodType.Decade2:
                            return new DateTime(Year, Number, 20);

                        case BudgetaryPeriodType.Decade3:
                            return new DateTime(Year, Number, DateTime.DaysInMonth(Year, Number));

                        case BudgetaryPeriodType.Date:
                            return Date ?? DateTime.Today;

                        case BudgetaryPeriodType.None:
                            return DateTime.Today;

                        default:
                            return new DateTime(DateTime.Now.Year, 12, 31);
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    return DateTime.MinValue;
                }
            }
        }

        /// <summary>
        /// Распарсить период из строки
        /// </summary>
        /// <param name="period">Период</param>
        /// <returns></returns>
        public static BudgetaryPeriod ParsePaymentPeriod(string period)
        {
            var paymentPeriod = new BudgetaryPeriod();
            var splitedString = SafeValue(period).Split('.');
            if (splitedString.Length > 0)
                paymentPeriod.Type = ParsePaymentPeriodType(splitedString[0]);
            int number = GetDefaultNumberForPeriod(paymentPeriod.Type);
            if (splitedString.Length > 1)
                int.TryParse(splitedString[1], out number);
            int year = DateTime.Today.Year;
            if (splitedString.Length > 2)
                int.TryParse(splitedString[2], out year);

            paymentPeriod.Number = number;
            paymentPeriod.Year = year;
            return paymentPeriod;
        }

        private static int GetDefaultNumberForPeriod(BudgetaryPeriodType periodType)
        {
            const int amountMonthesInHalfYear = 6;
            int number = 0;
            DateTime dateNow = DateTime.Now;
            switch (periodType)
            {
                case BudgetaryPeriodType.Year:
                    number = dateNow.Year;
                    break;
                case BudgetaryPeriodType.Quarter:
                    number = ((dateNow.Month - 1) / 3) + 1;
                    break;
                case BudgetaryPeriodType.Month:
                    number = dateNow.Month;
                    break;
                case BudgetaryPeriodType.Decade1:
                    number = 1;
                    break;
                case BudgetaryPeriodType.Decade2:
                    number = 2;
                    break;
                case BudgetaryPeriodType.Decade3:
                    number = 3;
                    break;
                case BudgetaryPeriodType.HalfYear:
                    number = dateNow.Month > amountMonthesInHalfYear ? 2 : 1;
                    break;
            }

            return number;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(BudgetaryPeriod)) return false;
            return Equals((BudgetaryPeriod)obj);
        }

        public bool Equals(BudgetaryPeriod other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Type, Type) && other.Number == Number && other.Year == Year;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Type.GetHashCode();
                result = (result * 397) ^ Number;
                result = (result * 397) ^ Year;
                return result;
            }
        }

        public static bool operator ==(BudgetaryPeriod left, BudgetaryPeriod right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(BudgetaryPeriod left, BudgetaryPeriod right)
        {
            return !Equals(left, right);
        }

        public string Format()
        {
            switch (Type)
            {
                case BudgetaryPeriodType.NoPeriod:
                    return "0";
                case BudgetaryPeriodType.Year:
                    return $"{Type.GetAbbreviation()}.{Year}";
                case BudgetaryPeriodType.Date:
                    return $"{Type.GetAbbreviation()}.{Date:d.MM.yyyy}";
                case BudgetaryPeriodType.Month:
                case BudgetaryPeriodType.Quarter:
                case BudgetaryPeriodType.HalfYear:
                    return $"{Type.GetAbbreviation()}.{Number}.{Year}";
                default:
                    return "0";
            }
        }
    }
}
