namespace Moedelo.AccountingV2.Dto.Api.ClientData
{
    public class BusinessTripClientData : BaseClientData
    {
        public long Id { get; set; }

        public long DocumentBaseId { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public AdvanceDocumentClientData AdvanceDocument { get; set; }
    }
}