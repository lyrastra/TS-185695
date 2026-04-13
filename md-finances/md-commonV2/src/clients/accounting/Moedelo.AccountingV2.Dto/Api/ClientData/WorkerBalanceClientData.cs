using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Api.ClientData
{
    public class WorkerBalanceClientData : BaseClientData
    {
        public WorkerBalanceClientData()
        {
            this.Documents = new List<AdvancePaymentDocumentClientData>();
        }

        public IList<AdvancePaymentDocumentClientData> Documents { get; set; }

        public decimal ClosedPeriodSum { get; set; }
    }
}