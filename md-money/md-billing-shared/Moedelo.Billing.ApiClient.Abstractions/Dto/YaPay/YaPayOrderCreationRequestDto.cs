namespace Moedelo.Billing.Abstractions.Dto.YaPay;

public class YaPayOrderCreationRequestDto
{
    public string BillNumber { get; set; }
        
    public string AfterErrorRedirectUrl { get; set; }
        
    public string AfterPaymentRedirectUrl { get; set; }
}