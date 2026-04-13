namespace Moedelo.Docs.ApiClient.Abstractions.NdsDeduction.Models
{
    public class DeductionAcceptedDto
    {
        public long DocumentBaseId { get; set; }
        public decimal NdsSum { get; set; }
        public decimal NdsDeductionAccepted { get; set; }
    }
}