namespace Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;

public class SaveReconciliationTempFileDto
{
    public string FileName { get; set; }
    
    public byte[] FileData { get; set; }
}