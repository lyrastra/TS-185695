namespace Moedelo.BillingV2.Dto.BackofficeBilling.Bills.BillRequest
{
    public class ModifierRequestDto
    {
        /// <summary>
        /// Имя градации модификатора
        /// </summary>
        public string GradationName { get; set; }

        /// <summary>
        /// Масштабирующее значение
        /// </summary>
        public decimal? GradationScaleValue { get; set; }
    }
}
