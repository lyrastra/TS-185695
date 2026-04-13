using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Registry.Domain.Models
{
    public class OperationDbModel
    {
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Номер документа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата операции
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        public int? ContractorId { get; set; }

        public  int ContractorType { get; set; }

        public string ContractorName { get; set; }

        /// <summary>
        /// Тип: поступление/списание
        /// </summary>
        public MoneyDirection Direction { get; set; }

        public int SourceId { get; set; }

        public OperationSource SourceType { get; set; }

        public string SourceName { get; set; }

        public int? KontragentSettlementAccountId { get; set; }

        /// <summary>
        /// Идентификатор типа операции
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата последнего изменения
        /// </summary>
        public DateTime ModifyDate { get; set; }

        /// <summary>
        /// Статус оплаты
        /// </summary>
        public bool IsPaid { get; set; }

        public int TotalCount { get; set; }

        /// <summary>
        /// Идентификатор привязанного патента
        /// </summary>
        public long? PatentId { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        /// <summary>
        /// В том числе НДС
        /// </summary>
        public bool IncludeNds { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal? NdsSum { get; set; }

        /// <summary>
        /// Ставка НДС
        /// </summary>
        public NdsType? NdsType { get; set; }

        /// <summary>
        /// Счёт контрагента
        /// </summary>
        public int? KontragentAccountCode { get; set; }
    }
}
