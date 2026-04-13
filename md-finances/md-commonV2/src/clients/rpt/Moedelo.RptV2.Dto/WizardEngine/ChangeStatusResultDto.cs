namespace Moedelo.RptV2.Dto.WizardEngine
{
    public class ChangeStatusResultDto
    {
        public enum StatusValue
        {
            Changed = 0,
            CannotBeChanged = 1,
            ConfirmationRequired = 2,
            RawError = 13
        }

        public StatusValue Status { get; set; } = StatusValue.Changed;
        public string UserMessage { get; set; } = string.Empty;
    }
}
