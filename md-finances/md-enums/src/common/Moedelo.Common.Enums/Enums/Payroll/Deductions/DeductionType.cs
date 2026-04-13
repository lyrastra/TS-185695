using System;
using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Payroll.Deductions
{
    public enum DeductionType : byte
    {
        [Description("Алименты")]
        Aliment = 0,
        [Description("Прочие удержания")]
        Other = 1
    }
    
    public static class DeductionTypeExtension
    {
        public static string ToText(this DeductionType type)
        {
            switch (type)
            {
                case DeductionType.Aliment:
                    return "Aliment";
                case DeductionType.Other:
                    return "Other";
                default:
                    throw new ArgumentOutOfRangeException($"Значение не найдено в {nameof(type)}");
            }
        }
    }
}