using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Api.Models.PaymentOrders.Outgoing.PaymentToSupplier
{
    public class PaymentToSupplierTaxPostingsGenerateRequestDto
    {
        /// <summary>
        /// Дата документа
        /// </summary>
        [DateValue]
        [RequiredValue]
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер п/п
        /// </summary>
        [ValidateXss]
        [RequiredValue]
        public string Number { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        [RequiredValue]
        public decimal Sum { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        [RequiredValue]
        public ContractorDto Contractor { get; set; }

        /// <summary>
        /// НДС
        /// </summary>
        public NdsDto Nds { get; set; }

        /// <summary>
        /// Связанные первичные документы
        /// </summary>
        public IReadOnlyCollection<DocumentLinkDto> Documents { get; set; } = Array.Empty<DocumentLinkDto>();

        /// <summary>
        /// Признак: п/п оплачено
        /// </summary>
        public bool IsPaid { get; set; }
    }
}
