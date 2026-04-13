namespace Moedelo.Money.Domain.LinkedDocuments
{
    public class BillLinkSaveRequest
    {
        public long BillBaseId { get; set; }

        public decimal LinkSum { get; set; }
    }
}