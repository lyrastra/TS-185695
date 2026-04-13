namespace Moedelo.RptV2.Dto.WizardAutoCompletion
{
    public class AutoUsnCompleteWizardResponseDto : AutoCompleteWizardResponseDto
    {
        public bool HasActiveIntegration { get; set; }

        public bool PaymentSentToBank { get; set; }

        public bool PaymentSentToBankError { get; set; }

        public bool PaymentSentToFns { get; set; }

        /// <summary>
        /// Сумма налога к уплате
        /// </summary>
        public decimal TaxSumForPayment { get; set; }

        /// <summary>
        /// Сумма начиленного налога для отправки в ФНС
        /// </summary>
        public decimal TaxSumForFns { get; set; }

        /// <summary>
        /// Отправлен через представителя по МЧД
        /// </summary>
        public bool IsSentByETrust { get; set; }

        /// <summary>
        /// Автопрохождение может быть перезапущено
        /// </summary>
        public bool CanBeRestarted { get; set; }
    }
}
