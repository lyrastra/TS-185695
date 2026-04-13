using System;
using System.Collections.Generic;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Dto
{
    public class PaymentFromCustomerAccPostingsGenerateRequestDto
    {
        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма платежа
        /// </summary>
        public decimal Sum { get; set; }

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
        /// Посредничество (УСН)
        /// </summary>
        public MediationDto Mediation { get; set; }

        /// <summary>
        /// Связанные первичные документы
        /// </summary>
        public IReadOnlyCollection<PaymentFromCustomerGenerateRequestDocumentLinkDto> Documents { get; set; } = Array.Empty<PaymentFromCustomerGenerateRequestDocumentLinkDto>();

        /// <summary>
        /// Признак: основной контрагент
        /// </summary>
        public bool? IsMainContractor { get; set; }
    }
}
