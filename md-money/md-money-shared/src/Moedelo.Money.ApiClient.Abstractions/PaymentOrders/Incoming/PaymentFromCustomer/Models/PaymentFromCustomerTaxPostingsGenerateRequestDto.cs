using System;
using System.Collections.Generic;
using Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Dto;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Dto
{
    public class PaymentFromCustomerTaxPostingsGenerateRequestDto
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
        /// НДС для посредничества
        /// </summary>
        public NdsDto MediationNds { get; set; }

        /// <summary>
        /// Посредничество (УСН)
        /// </summary>
        public MediationDto Mediation { get; set; }

        /// <summary>
        /// Связанные первичные документы
        /// </summary>
        public IReadOnlyCollection<PaymentFromCustomerGenerateRequestDocumentLinkDto> Documents { get; set; } =
            Array.Empty<PaymentFromCustomerGenerateRequestDocumentLinkDto>();

        /// <summary>
        /// Учитывать в СНО
        /// УСН = 1
        /// ОСНО = 2
        /// ЕНВД = 3
        /// УСН + ЕНВД = 4
        /// ОСНО + ЕНВД = 5
        /// </summary>
        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}
