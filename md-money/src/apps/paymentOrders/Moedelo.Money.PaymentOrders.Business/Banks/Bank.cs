namespace Moedelo.Money.PaymentOrders.Business.Banks
{
    internal class Bank
    {
        public int Id { get; set; }

        public string Bik { get; set; }

        public string Kpp { get; set; }

        public string Inn { get; set; }

        public string CorrespondentAccount { get; set; }

        public string FullName { get; set; }

        public string City { get; set; }

        public string FullNameWithCity { get; set; }
    }
}
