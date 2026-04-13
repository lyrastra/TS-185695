namespace Moedelo.AccountingV2.Dto.Money
{
    public class KontragentForMoneyReplaceItemDto
    {
        public int NewKontragentId { get; set; }
        public string NewKontragentName { get; set; }
        public int? NewKontragentSettlementAccountId { get; set; }
        public int OldKontragentId { get; set; }
        public int? OldKontragentSettlementAccountId { get; set; }
    }
}
