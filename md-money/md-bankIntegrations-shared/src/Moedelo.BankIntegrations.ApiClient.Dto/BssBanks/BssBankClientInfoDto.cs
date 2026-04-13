using Moedelo.BankIntegrations.Enums.BssSso;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.Dto.BssBanks
{
    public class BssBankClientInfoDto : BaseResponseDto
    {
        public string Id { get; set; }
        public string Kpp { get; set; }
        public string Inn { get; set; }
        public string ClientName { get; set; }
        public string LeadName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DboClientId { get; set; }
        public TaxationSystem TaxSystem { get; set; }
        public LegalForm LegalForm { get; set; }
        public IEnumerable<BssBankAccountDto> Accounts { get; set; }
        public BssBankTokenDto Token { get; set; }
    }
}
