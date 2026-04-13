
namespace Moedelo.RptV2.Dto.UsnDeclaration
{
    public class UsnTaxProfitAndExpenseSumDto
    {
        /// <summary>
        /// Прибыль
        /// </summary>
        public decimal Benefit { get; set; }
        /// <summary>
        /// Налогооблагаемая база
        /// </summary>
        public decimal TaxBase { get; set; }
        /// <summary>
        /// Исчисленный в общем порядке налог
        /// </summary>
        public decimal TaxSumByRate { get; set; }
        /// <summary>
        /// Минимальный налог
        /// </summary>
        public decimal TaxSumMinimal { get; set; }
        /// <summary>
        /// Начисленный налог
        /// </summary>
        public decimal TaxSum { get; set; }
        /// <summary>
        /// Сумма налога к уплате
        /// </summary>
        public decimal TaxSumForPayment { get; set; }
        /// <summary>
        /// Максимальное значение убытки прошлых лет
        /// </summary>
        public decimal MaxDecreaseSum { get; set; }

    }
}
