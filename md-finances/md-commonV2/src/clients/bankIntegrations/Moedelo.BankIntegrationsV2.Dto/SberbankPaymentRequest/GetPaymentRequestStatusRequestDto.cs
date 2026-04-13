using System;
using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class GetPaymentRequestStatusRequestDto
    {
        /// <summary> Идентификатор документа, по которому нужно получить статус </summary>
        public List<SberbankPaymentRequestStatusItem> ExternalDocuments { get; set; }
    }
}