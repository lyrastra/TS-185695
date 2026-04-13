namespace Moedelo.ErptV2.Dto.Eds
{
    public class EdsWizardStateResponse
    {
        public EdsWizardStateDto EdsWizardStateDto { get; set; }
        public int EventId { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}