namespace Moedelo.RequisitesV2.Dto.TradingObjects
{
    public class TradingObjectV2WizardDto
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public string Name { get; set; }

        public TradingObjectAddressDto Address { get; set; }

        public int IFNS { get; set; }

        public string OKTMO { get; set; }

        /// <summary>
        /// Сумма торгового сбора за квартал
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Оплаченная сумма за квартал. !!Только для платформы БИЗ!!
        /// </summary>
        public decimal PayedSumForBiz { get; set; }
    }
}