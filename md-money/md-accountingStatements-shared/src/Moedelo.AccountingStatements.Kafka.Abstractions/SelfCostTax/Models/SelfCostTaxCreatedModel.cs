using System;

namespace Moedelo.AccountingStatements.Kafka.Abstractions.SelfCostTax.Models
{
    /// <summary>
    /// Модель созданной бухсправки с типом "Себестоимость НУ (ФИФО)"
    /// </summary>
    public class SelfCostTaxCreatedModel
    {
        /// <summary>
        /// BaseId бух. справки
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата бух. справки
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер бух. справки
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Сумма бух. справки
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// BaseId связанного документа
        /// </summary>
        public long LinkedDocumentBaseId { get; set; }
    }
}