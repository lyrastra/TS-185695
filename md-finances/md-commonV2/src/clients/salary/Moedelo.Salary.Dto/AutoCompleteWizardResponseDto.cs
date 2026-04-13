using System.Collections.Generic;

namespace Moedelo.Salary.Dto
{
    public class AutoCompleteWizardResponseDto
    {
        public bool IsSuccess { get; set; }

        public List<AutoCompleteValidationMessageDto> Messages { get; set; } =
            new List<AutoCompleteValidationMessageDto>();
    }
}