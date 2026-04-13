using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Dto
{
    public class PaymentToSupplierTaxPostingsGenerateRequestDto
    {
        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Номер п/п
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Контрагент
        /// </summary>
        public ContractorDto Contractor { get; set; }

        /// <summary>
        /// НДС
        /// </summary>
        public NdsDto Nds { get; set; }

        /// <summary>
        /// Связанные первичные документы
        /// </summary>
        public IReadOnlyCollection<PaymentToSupplierGenerateRequestDocumentLinkDto> Documents { get; set; } = Array.Empty<PaymentToSupplierGenerateRequestDocumentLinkDto>();

        /// <summary>
        /// Признак: п/п оплачено
        /// </summary>
        public bool IsPaid { get; set; }
    }
}
