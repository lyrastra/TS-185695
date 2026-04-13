namespace Moedelo.Estate.Client.InventoryCard.Dto
{
    public class InventoryCardFromBalancesSaveDto
    {
        public long SubcontoId { get; set; }

        public decimal BalanceSum { get; set; }

        public int SyntheticAccountCode { get; set; }
    }
}