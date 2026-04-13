using System;

namespace Moedelo.BankIntegrations.Dto.Payments
{
    public class ExternalDocumentDto
    {
        /// <summary> Идентификатор платежа в МоёДело </summary>
        public Guid Id { get; set; }

        /// <summary> Идентификатор платежа присвоенный банком </summary>
        public string ExternalId { get; set; }
        
        /// <summary> Идентификатор запроса платежа присвоенный банком </summary>
        public string ExternalRequestId { get; set; }

        /// <summary> Описание статуса платежа из банка </summary>
        public string DescriptionStatus { get; set; }

        /// <summary> Url адрес в ДБО на конкретное п/п, для оплаты клиентом </summary>
        public string InvoiceUrl { get; set; }
    }
}