namespace Moedelo.KudirOsno.Client.FixedAsset.Dto
{
    public class InventoryCardExpenseSummaryDto
    {
        /// <summary>
        /// BaseId инвентарной карты
        /// </summary>
        public long DocumentBaseId { get; set; }
        
        /// <summary>
        /// Списанная в амортизацию сумма
        /// </summary>
        public decimal AmortizedSum { get; set; }
        
        /// <summary>
        /// Остаток амортизации
        /// </summary>
        public decimal AmortizationRemainSum { get; set; }
    }
}
