using Moedelo.TaxPostings.Dto.Postings.Money.Dto;
using System;
using System.Collections.Generic;

namespace Moedelo.TaxPostings.Dto.Postings.Money.Incoming.Dto
{
    public class PaymentFromCustomerGenerateRequestDto
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
        /// Контрагент
        /// </summary>
        public ContractorDto Contractor { get; set; }

        /// <summary>
        /// НДС
        /// </summary>
        public NdsDto Nds { get; set; }

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
        public int? TaxationSystemType { get; set; }
    }
}
