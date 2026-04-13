using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.PostingEngine;
using System;

namespace Moedelo.Money.Dto
{
    public class OperationResponseDto
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
        public ContractorDto Contractor { get; set; }

        /// <summary>
        /// Тип: поступление/списание
        /// 1 - Списание
        /// 2 - Поступление
        /// </summary>
        public PaymentDirection Direction { get; set; }

        /// <summary>
        /// Источник: банк/касса/платежные системы
        /// </summary>
        public SourceDto Source { get; set; }

        /// <summary>
        /// https://github.com/moedelo/md-money/blob/master/src/apps/Moedelo.Money.Api/Models/Registry/OperationResponseDto.cs
        /// </summary>
        public OperationType OperationType { get; set; }

        /// <summary>
        /// Назначение платежа
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Статус оплаты
        /// </summary>
        public bool IsPaid { get; set; }
        
        /// <summary>
        /// Идентификатор привязанного патента
        /// </summary>
        public long? PatentId { get; set; }
        
        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}