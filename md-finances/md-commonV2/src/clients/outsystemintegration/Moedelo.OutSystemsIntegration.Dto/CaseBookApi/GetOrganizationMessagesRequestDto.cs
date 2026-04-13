using Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi.Bankruptcy;
using System;

namespace Moedelo.OutSystemsIntegrationV2.Dto.CaseBookApi
{
    public class GetOrganizationMessagesRequestDto
    {
        public string Inn { get; set; }

        public string Ogrn { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public BankruptTypeEnum? BankruptType { get; set; }
    }
}