namespace Moedelo.AccountingV2.Dto.PurchaseInfo
{
    public class PurchaseInfoRequestDto
    {
        public long RequestId { get; set; }
        public int KontragentId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
