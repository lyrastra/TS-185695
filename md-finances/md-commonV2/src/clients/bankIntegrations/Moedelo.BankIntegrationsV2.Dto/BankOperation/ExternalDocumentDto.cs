using System;

namespace Moedelo.BankIntegrationsV2.Dto.BankOperation
{
    public class ExternalDocumentDto
    {
        public Guid Id { get; set; }
        public string ExternalId { get; set; }
        public string Error { get; set; }

        /// <summary> Url адрес ДБО для оплаты выставленного счета </summary>
        public string InvoiceUrl { get; set; }
    }
}