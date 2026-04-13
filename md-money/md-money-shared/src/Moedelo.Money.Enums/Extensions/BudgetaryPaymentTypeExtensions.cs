using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.Enums.Extensions
{
    public static class BudgetaryPaymentTypeExtensions
    {
        private static readonly Dictionary<BudgetaryPaymentType, string> PaymentTypeName =
            new Dictionary<BudgetaryPaymentType, string>
            {
                {BudgetaryPaymentType.TaxPayment, "НС"},
                {BudgetaryPaymentType.Fee, "ВЗ"},
                {BudgetaryPaymentType.Pay, "ПЛ"},
                {BudgetaryPaymentType.Advance, "АВ"},
                {BudgetaryPaymentType.Duty, "ГП"},
                {BudgetaryPaymentType.Peni, "0"},
                {BudgetaryPaymentType.TaxSanction, "СА"},
                {BudgetaryPaymentType.AdministrationPenalty, "АШ"},
                {BudgetaryPaymentType.AnotherPenalty, "ИШ"},
                {BudgetaryPaymentType.Percent, "ПЦ"},
                {BudgetaryPaymentType.Other, "0"}
            };

        public static string ToText(this BudgetaryPaymentType type)
        {
            return PaymentTypeName.ContainsKey(type) ? PaymentTypeName[type] : string.Empty;
        }

        public static BudgetaryPaymentType ToEnum(string data)
        {
            return string.IsNullOrEmpty(data)
                ? BudgetaryPaymentType.None
                : PaymentTypeName.FirstOrDefault(x => x.Value == data).Key;
        }
    }
}