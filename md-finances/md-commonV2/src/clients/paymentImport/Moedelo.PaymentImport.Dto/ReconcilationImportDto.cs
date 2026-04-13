using System;
using System.Collections.Generic;

namespace Moedelo.PaymentImport.Dto
{
    public class ReconcilationImportDto
    {
        public int SettlementAccountId { get; set; }
        public IList<string> Documents { get; set; }
        public Guid SessionId { get; set; }
    }
}
