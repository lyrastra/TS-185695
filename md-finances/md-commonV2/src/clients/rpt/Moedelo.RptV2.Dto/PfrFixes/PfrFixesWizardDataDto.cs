namespace Moedelo.RptV2.Dto.PfrFixes
{
    public class PfrFixesWizardDataDto
    {
        /// <summary>
        /// Идентификатор мастера
        /// </summary>
        public long WizardStateId { get; set; }
        /// <summary>
        /// Включена настройка оплаты доп. взноса
        /// </summary>
        public bool AdditionalPaymentVisible { get; set; }
    }
}