using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.AccountingV2.Dto.PrimaryDocuments
{
    public class ReasonDocumentsAutomationDto
    {
        public ReasonDocumentsAutomationDto(int kontragentId, PrimaryDocumentsTransferDirection direction)
        {
            KontragentId = kontragentId;
            Direction = direction;
            IsMainContractor = true;
        }

        public int KontragentId { get; private set; }

        public PrimaryDocumentsTransferDirection Direction { get; private set; }

        public long? BillBaseId { get; set; }

        public long? ContractBaseId { get; set; }

        public bool WithUpd { get; set; }

        /// <summary>
        /// Флаг основного котрагента
        /// </summary>
        public bool? IsMainContractor { get; set; }
    }
}
