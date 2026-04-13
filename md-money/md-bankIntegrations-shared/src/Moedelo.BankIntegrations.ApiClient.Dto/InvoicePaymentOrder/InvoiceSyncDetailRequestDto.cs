namespace Moedelo.BankIntegrations.ApiClient.Dto.InvoicePaymentOrder;

public class InvoiceSyncDetailRequestDto
{
    /// <summary> Внешний индетификатор документа </summary>
    public string ExternalPaymentId { get; set; }
    
    /// <summary> Номер платежного документа </summary>
    public string PaymentNumber { get; set; }
        
    /// <summary> Номер счета отправителя </summary>
    public string PayerSettlementNumber { get; set; }
    
    /// <summary> ИНН отправителя </summary>
    public string PayerInn  { get; set; }
        
    /// <summary> Номер счета получателя </summary>
    public string RecipientSettlementNumber  { get; set; }
        
    /// <summary> ИНН получателя </summary>
    public string RecipientInn  { get; set; }
        
    /// <summary> Сумма платежа </summary>
    public decimal Amount { get; set; }
}