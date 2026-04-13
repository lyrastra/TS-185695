namespace Moedelo.PaymentImport.Dto
{
    public class SaveMovementListDto
    {
        public int FirmId { get; set; }
    
        public string FileName { get; set; }
    
        public byte[] FileData { get; set; }
    }
}