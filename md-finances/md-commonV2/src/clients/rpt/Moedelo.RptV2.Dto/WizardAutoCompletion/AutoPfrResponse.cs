using Moedelo.Common.Enums.Enums.Reports;

namespace Moedelo.RptV2.Dto.WizardAutoCompletion
{
    public class AutoPfrResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public decimal Sum { get; set; }
        public UserCheckStatus CheckStatus { get; set; }
    }
}
