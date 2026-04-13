namespace Moedelo.CashV2.Dto.Cashbox
{
    public class CashboxDto
    {
        public int Id { get; set; }
        public long StockId { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// ID Кассы
        /// </summary>
        public long FirmCashId { get; set; }
    }
}