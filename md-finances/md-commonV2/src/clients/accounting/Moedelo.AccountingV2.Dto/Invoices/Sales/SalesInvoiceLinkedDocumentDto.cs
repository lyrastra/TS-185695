using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.Invoices.Sales
{
    public class SalesInvoiceLinkedDocumentDto
    {
        public long DocumentId { get; set; }

        public AccountingDocumentType Type { get; set; }
    }
}