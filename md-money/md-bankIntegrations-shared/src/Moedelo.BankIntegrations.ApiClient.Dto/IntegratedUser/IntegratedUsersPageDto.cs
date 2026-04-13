using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class IntegratedUsersPageDto
    {
        public List<ExtentedIntegratedUserDto> Data { get; set; }

        public int PageCount { get; set; }
    }
}
