using Moedelo.Accounting.Enums;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto
{
    public class FindReasonDocumentsAutomationDto
    {
        public FindReasonDocumentsAutomationDto(int kontragentId, PrimaryDocumentsTransferDirection direction)
        {
            KontragentId = kontragentId;
            Direction = direction;
        }

        public int KontragentId { get; }

        public PrimaryDocumentsTransferDirection Direction { get; }

        public long? BillBaseId { get; set; }

        public long? ContractBaseId { get; set; }

        public bool WithUpd { get; set; }

        /// <summary>
        /// Флаг основного котрагента
        /// </summary>
        public bool? IsMainContractor { get; set; }
    }
}