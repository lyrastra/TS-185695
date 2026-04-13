using Moedelo.Common.Enums.Enums.Reports;

namespace Moedelo.RptV2.Dto.WizardAutoCompletion
{
    public class AutoUsnResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public decimal Sum { get; set; }
        public UserCheckStatus CheckStatus { get; set; }

        public bool PaymentSentToBank { get; set; }

        public bool PaymentSentToBankError { get; set; }
    }
}