using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.SelfCostPayments
{
    /// <summary>
    /// Платеж для расчетов себестоимости
    ///  - оплата поставщику (банк/касса/валюта)
    ///  - выдача под отчет (банк/касса)
    ///  - бюджетный платеж по НДС (валюта)
    /// </summary>
    public class SelfCostPayment
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер платежа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата платежа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }
        
        /// <summary>
        /// сумма в рублях (для валютных банковских платежей)
        /// </summary>
        public decimal? RubSum { get; set; }
        
        /// <summary>
        /// Тип платежа
        /// </summary>
        public OperationType Type { get; set; }
    }
}