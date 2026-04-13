namespace Moedelo.AccountingV2.Dto.Cash
{
    public class CashDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public bool IsEnable { get; set; }
        public long? SubcontoId { get; set; }
    }
}