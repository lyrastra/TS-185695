using System.Collections.Generic;

namespace Moedelo.OfficeV2.Dto.BankruptcyCheck
{
    public class BankruptcyMessagesInfoDto
    {
        public int OrganizationMessagesCount { get; set; }

        public List<BankruptcyMessageDto> OrganizationMessages { get; set; }
    }
}