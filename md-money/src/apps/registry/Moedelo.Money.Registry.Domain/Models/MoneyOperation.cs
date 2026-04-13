using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Registry.Domain.Models
{
    public class MoneyOperation
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

        /// <summary>
        /// Контрагент
        /// </summary>
        public Contractor Contractor { get; set; }

        /// <summary>
        /// Тип: поступление/списание
        /// </summary>
        public MoneyDirection Direction { get; set; }

        /// <summary>
        /// Источник: банк/касса/платежные системы
        /// </summary>
        public Source Source { get; set; }

        public int? KontragentSettlementAccountId { get; set; }

        /// <summary>
        /// Тип операции
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

        /// <summary>
        /// Идентификатор привязанного патента
        /// </summary>
        public long? PatentId { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public bool IncludeNds { get; set; }

        public decimal? NdsSum { get; set; }

        public NdsType? NdsType { get; set; }

        /// <summary>
        /// Счёт контрагента
        /// </summary>
        public int? KontragentAccountCode { get; set; }
    }
}
