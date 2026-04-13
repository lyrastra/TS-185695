using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Registry.Dto
{
    /// <summary>
    /// Модель для хранения запроса по реестру операций
    /// </summary>
    public class RegistryOperationDto
    {
        /// <summary>
        /// Идентификатор операции
        /// </summary>
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
        public RegistryOperationContractorDto Contractor { get; set; }

        /// <summary>
        /// Тип: поступление/списание
        /// </summary>
        public MoneyDirection Direction { get; set; }

        /// <summary>
        /// Источник: банк/касса/платежные системы
        /// </summary>
        public RegistryOperationSourceDto Source { get; set; }

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

        /// <summary>
        /// Тип системы налогообложения
        /// </summary>
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
        /// Тип НДС
        /// </summary>
        public NdsType? NdsType { get; set; }

        /// <summary>
        /// Основной контрагент
        /// </summary>
        public bool IsMainContractor { get; set; }
    }
}
