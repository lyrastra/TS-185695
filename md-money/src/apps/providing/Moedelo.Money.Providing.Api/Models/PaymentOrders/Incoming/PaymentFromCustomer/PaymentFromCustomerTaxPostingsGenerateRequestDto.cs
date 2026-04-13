using System;
using System.Collections.Generic;
using Moedelo.Infrastructure.AspNetCore.Validation;
using Moedelo.Infrastructure.AspNetCore.Xss.Validation;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Providing.Api.Models.PaymentOrders.Incoming.PaymentFromCustomer
{
    public class PaymentFromCustomerTaxPostingsGenerateRequestDto
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
        public IReadOnlyCollection<DocumentLinkDto> Documents { get; set; } = Array.Empty<DocumentLinkDto>();

        /// <summary>
        /// Учитывать в СНО
        /// УСН = 1
        /// ОСНО = 2
        /// ЕНВД = 3
        /// УСН + ЕНВД = 4
        /// ОСНО + ЕНВД = 5
        /// </summary>
        [EnumValue(EnumType = typeof(TaxationSystemType), AllowNull = true)]
        public TaxationSystemType? TaxationSystemType { get; set; }
    }
}
