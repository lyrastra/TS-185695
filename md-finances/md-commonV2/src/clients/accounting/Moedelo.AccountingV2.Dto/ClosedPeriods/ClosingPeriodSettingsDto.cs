namespace Moedelo.AccountingV2.Dto.ClosedPeriods
{
    public class ClosingPeriodSettingsDto
    {
        /// <summary>
        /// Год, начиная с которого ИП на УСН показываются события по закрытию периода
        /// </summary>
        public int? IpStartYear { get; set; }
    }
}