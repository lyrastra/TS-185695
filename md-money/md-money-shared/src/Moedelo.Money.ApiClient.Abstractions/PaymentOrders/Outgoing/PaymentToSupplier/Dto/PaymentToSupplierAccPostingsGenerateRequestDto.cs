using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Dto
{
    public class PaymentToSupplierAccPostingsGenerateRequestDto
    {
        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Идентификатор расчётного счёта
        /// </summary>
        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Реквизиты контрагента
        /// </summary>
        public ContractorDto Contractor { get; set; }

        /// <summary>
        /// Договор
        /// </summary>
        public ContractDto Contract { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Признак: основной контрагент
        /// </summary>
        public bool? IsMainContractor { get; set; }

        /// <summary>
        /// Связанные первичные документы
        /// </summary>
        public IReadOnlyCollection<PaymentToSupplierGenerateRequestDocumentLinkDto> Documents { get; set; } = Array.Empty<PaymentToSupplierGenerateRequestDocumentLinkDto>();
    }
}
