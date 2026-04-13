using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Common.Enums.Enums.Accounting
{
    /// <summary>
    /// Тип бюджетного платежа
    /// </summary>
    public enum BudgetaryPaymentType
    {
        /// <summary>
        /// Небюджетный платеж
        /// </summary>
        None = 0,

        /// <summary> Налоговый сбор (НС) </summary>
        TaxPayment = 1,

        /// <summary> Взнос (ВЗ) </summary>
        Fee = 2,

        /// <summary> уплата платежа (ПЛ) </summary>
        Pay = 3,

        /// <summary> уплата аванса или предоплата(АВ) </summary>
        Advance = 4,

        /// <summary> ГП - уплата пошлины; (При выборе прочее) </summary>
        Duty = 5,

        /// <summary> ПЕ - уплата пени; (для всех) </summary>
        Peni = 6,

        /// <summary> СА - налоговые санкции, установленные Налоговым кодексом Российской Федерации; (для ИФНС и и прочего) </summary>
        TaxSanction = 7,

        /// <summary> АШ - административные штрафы; (для всех) </summary>
        AdministrationPenalty = 8,

        /// <summary> ИШ - иные штрафы, установленные соответствующими законодательными или иными нормативными актами. (для Всех) </summary>
        AnotherPenalty = 9,

        /// <summary> ПЦ - уплата процентов; (прочие) </summary>
        Percent = 10,

        /// <summary>
        /// 0 - остальные типы платежей
        /// </summary>
        Other = 11
    }
    
    public static class BudgetaryPaymentTypeExtensions
    {
        private static readonly Dictionary<BudgetaryPaymentType, string> paymentTypeName = new Dictionary<BudgetaryPaymentType, string>
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
            if (!paymentTypeName.ContainsKey(type))
            {
                return string.Empty;
            }

            return paymentTypeName[type];
        }

        public static BudgetaryPaymentType ToEnum(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return BudgetaryPaymentType.None;
            }
            return paymentTypeName.FirstOrDefault(x => x.Value == data).Key;
        }
    }
}