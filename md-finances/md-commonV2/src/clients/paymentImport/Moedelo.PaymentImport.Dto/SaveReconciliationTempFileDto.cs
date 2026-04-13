namespace Moedelo.PaymentImport.Dto
{
    public class SaveReconciliationTempFileDto
    {
        public string FileName { get; set; }
    
        public byte[] FileData { get; set; }
    }
}