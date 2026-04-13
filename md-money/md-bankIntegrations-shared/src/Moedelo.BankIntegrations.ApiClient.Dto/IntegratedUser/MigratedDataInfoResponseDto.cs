using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class MigratedDataInfoResponseDto
    {
        public int FirmId { get; set; }
        public string Inn { get; set; }
        public string CustomerCode { get; set; }
        public string Login { get; set; }
        public string ErrorMessageByToArchivedSettlementAccounts { get; set; }
        public IReadOnlyCollection<int> ToArchivedSettlementAccountIds { get; set; }
        public IReadOnlyCollection<int> CreatedSettlementAccountIds { get; set; }
    }
}
