using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.IntegratedUser
{
    public class IntegratedUsersPageDto
    {
        public List<IntegratedUserDto> Data { get; set; }

        public int PageCount { get; set; }
    }
}
