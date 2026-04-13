namespace Moedelo.BackofficeV2.Dto.SendBill
{
    public class SendBillResponseDto
    {
        public int FirmId { get; set; }
        
        public int[] PaymentHistoryIds { get; set; }
         
        public bool Sended { get; set; } 
    }
}