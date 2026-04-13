using Moedelo.Common.Enums.Enums.Wizard;

namespace Moedelo.RptV2.Dto.AutoWizardCompletion
{
    public class AutoWizardCompletionParamsDto
    {
        public int FirmId { get; set; }        
        public int UserId { get; set; }        
        public string Login { get; set; }
        public WizardType WizardType { get; set; }
        public int ReportPeriod { get; set; }
        public int ReportYear { get; set; }
    }
}