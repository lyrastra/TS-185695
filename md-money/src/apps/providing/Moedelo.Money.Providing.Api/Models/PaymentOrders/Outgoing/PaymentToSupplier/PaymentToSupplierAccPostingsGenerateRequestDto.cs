using Moedelo.Infrastructure.AspNetCore.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Moedelo.Money.Providing.Api.Models.PaymentOrders.Outgoing.PaymentToSupplier
{
    public class PaymentToSupplierAccPostingsGenerateRequestDto
    {
        /// <summary>
        /// Дата документа
        /// </summary>
        [DateValue]
        [RequiredValue]
        public DateTime Date { get; set; }

        /// <summary>
        /// Идентификатор расчётного счёта
        /// </summary>
        [IdIntValue]
        [RequiredValue]
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Реквизиты контрагента
        /// </summary>
        [RequiredValue]
        public ContractorDto Contractor { get; set; }

        /// <summary>
        /// Договор
        /// </summary>
        public ContractDto Contract { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        [SumValue]
        [RequiredValue]
        public decimal Sum { get; set; }

        /// <summary>
        /// Признак: основной контрагент
        /// </summary>
        [DefaultValue(true)]
        public bool? IsMainContractor { get; set; }

        /// <summary>
        /// Связанные первичные документы
        /// </summary>
        public IReadOnlyCollection<DocumentLinkDto> Documents { get; set; } = Array.Empty<DocumentLinkDto>();
    }
}
