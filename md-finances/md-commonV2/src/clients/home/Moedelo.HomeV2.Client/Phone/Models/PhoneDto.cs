using Moedelo.Common.Enums.Enums.Requisites;

namespace Moedelo.HomeV2.Dto.Phone
{
    public class PhoneDto
    {
        public int Id { get; set; }

        public int FirmId { get; set; }
        
        public string Number { get; set; }

        public string ConfirmCode { get; set; }

        public PhoneTypes Type { get; set; }

        public byte? ConfirmCodeSendNumber { get; set; }

        public bool IsConfirm { get; set; }
    }
}
