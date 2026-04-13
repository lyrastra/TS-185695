using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.ApiClient.Abstractions.Money.Dto
{
    public class RegistryOperationDto
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
        public RegistryContractorDto Contractor { get; set; }

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
        /// В том числе НДС
        /// </summary>
        public NdsDto Nds { get; set; }

        /// <summary>
        /// Основной контрагент
        /// </summary>
        public bool IsMainContractor { get; set; }

        /// <summary>
        /// Выбранная СНО в операции
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}
