using System.Collections.Generic;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Enums.BssSso;

namespace Moedelo.BankIntegrations.ApiClient.Dto.SdmBank
{
    public class ClientInfoDto : BaseResponseDto
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
        public IEnumerable<BankAccountDto> Accounts { get; set; }
        public TokenDto Token { get; set; }
    }
}