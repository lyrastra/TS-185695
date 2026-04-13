using Moedelo.AccountingV2.Dto.Api.ClientData;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.TaxPostings;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.PurseOperation
{
    public class PurseOperationClientDataDto
    {
        public int Id { get; set; }

        public long? DocumentBaseId { get; set; }

        public string Number { get; set; }

        public string Date { get; set; }

        public decimal Sum { get; set; }

        public decimal? UsnSum { get; set; }

        public PurseOperationType PurseOperationType { get; set; }

        public long? ContractBaseId { get; set; }

        public string ProjectNumber { get; set; }

        public int? KontragentId { get; set; }

        public long? KontragetnSubcontoId { get; set; }

        public string KontragentName { get; set; }

        public int PurseId { get; set; }

        public int SettlementAccountId { get; set; }

        public string Comment { get; set; }

        public long? BillDocumentBaseId { get; set; }

        public TaxationSystemType? TaxSystemType { get; set; }

        public List<TaxPostingClientData> TaxPostings { get; set; }

        public bool ProvideInAccounting { get; set; }

        public TaxPostingStatus TaxStatus { get; set; }

        public ProvidePostingType PostingsAndTaxMode { get; set; }

        public long? PatentId { get; set; }

        /// <summary>
        /// В том числе НДС
        /// </summary>
        public bool? IncludeNds { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal? NdsSum { get; set; }

        public long waybillId { get; set; }

    }
}
