using Moedelo.Common.Enums.Enums.Account;

namespace Moedelo.AccountV2.Dto.ProfOutsource
{
    public class InviteDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public InviteStatus Status { get; set; }
        public string Message { get; set; }
        public string ProfOutsourceName { get; set; }
        public string FirmName { get; set; }
        public string ProfOutsourceEmail { get; set; }
        public string Phone { get; set; }
    }
}
