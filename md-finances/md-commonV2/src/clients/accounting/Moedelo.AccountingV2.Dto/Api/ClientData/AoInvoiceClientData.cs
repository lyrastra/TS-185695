using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Api.ClientData
{
    public class AoInvoiceClientData
    {
        public int? Id { get; set; }

        public long? DocumentBaseId { get; set; }

        public string InvoiceNumber { get; set; }

        public string InvoiceDate { get; set; }

        /// <summary> Принять НДС к вычету </summary>
        public bool UseNdsMinus { get; set; }

        /// <summary> Сумма НДС к вычету </summary>
        public decimal NdsDeductionSum { get; set; }

        public int KontragentId { get; set; }

        public string KontragentName { get; set; }

        public IList<AoInvoiceItemClientData> Items { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(this.InvoiceNumber) || string.IsNullOrEmpty(this.InvoiceDate))
                return this.Id.GetValueOrDefault() > 0;
            return true;
        }
    }
}