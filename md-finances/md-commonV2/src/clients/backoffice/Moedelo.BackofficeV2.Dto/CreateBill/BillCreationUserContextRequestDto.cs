namespace Moedelo.BackofficeV2.Dto.CreateBill
{
    public class BillCreationUserContextRequestDto
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public BillCreationRequestRequestDto Request { get; set; }
    }
}