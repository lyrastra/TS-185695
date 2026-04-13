namespace Moedelo.Money.Business.Cash
{
    internal sealed class CashModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsMain { get; set; }

        public bool IsEnable { get; set; }

        public long? SubcontoId { get; set; }
    }
}