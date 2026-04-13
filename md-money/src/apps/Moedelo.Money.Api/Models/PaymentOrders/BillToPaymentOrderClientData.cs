namespace Moedelo.Money.Handler.Dto
{
    public class BillToPaymentOrderClientData
    {
        public decimal Price { get; set; }

        public int RecipientId { get; set; }

        public string RecipientName { get; set; }

        public string RecipientInn { get; set; }

        public string RecipientKpp { get; set; }
    }
}
