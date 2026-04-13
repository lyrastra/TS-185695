namespace Moedelo.RptV2.Dto.FixedPayments
{
    public class FixedPaymentsWizardDataDto
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