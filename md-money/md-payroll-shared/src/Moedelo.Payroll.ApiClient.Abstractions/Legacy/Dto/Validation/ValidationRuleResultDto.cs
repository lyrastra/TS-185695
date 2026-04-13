namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Validation
{
    public class ValidationRuleResultDto
    {
        public ValidationRuleResultDto(string propertyName, string errorMessage, 
            ValidationRuleGroup group = ValidationRuleGroup.None)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            Group = group;
        }
        public string PropertyName { get; }

        public string ErrorMessage { get; }
        
        public ValidationRuleGroup Group { get; }
    }
}