namespace Moedelo.Money.Domain.LinkedDocuments
{
    public class SetReserveRequest
    {
        public long DocumentBaseId { get; set; }
        public decimal? ReserveSum { get; set; }
    }
}