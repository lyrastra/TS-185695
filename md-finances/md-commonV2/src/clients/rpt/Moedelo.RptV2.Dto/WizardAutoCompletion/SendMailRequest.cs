using Moedelo.Common.Enums.Enums.Reports;
using Moedelo.Common.Enums.Enums.Wizard;

namespace Moedelo.RptV2.Dto.AutoWizardCompletion
{
    public class SendMailRequest
    {
        public int FirmId { get; set; }
        
        public int UserId { get; set; }
        
        public WizardType WizardType { get; set; }
        
        /// <summary>Успешность завершения мастера</summary>
        public bool IsWizardCompetionSuccess { get; set; }
        
        public int WizardPeriod { get; set; }
        
        public int WizardYear { get; set; }

        /// <summary>Сумма, к уплате</summary>
        public decimal RemainingSum { get; set; }

        /// <summary>Набор ошибок, найденных при завершении мастера</summary>
        public UserCheckStatus CheckStatus { get; set; }
    }
}