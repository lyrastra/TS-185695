using System.Collections.Generic;

namespace Moedelo.RptV2.Dto.WizardAutoCompletion
{
    public class AutoCompleteWizardResponseDto
    {
        public bool IsSuccess { get; set; }

        public bool IsReportSent { get; set; }

        public bool IsError { get; set; }
        
        public List<AutoCompleteWizardValidationMessageDto> Messages { get; set; } = new List<AutoCompleteWizardValidationMessageDto>();
    }
}
