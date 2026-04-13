using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Tinkoff
{
    public class IntegrationDataDto
    {
        public int FirmId { get; set; }

        public TokenDto Token { get; set; }

        public DateTime SessionLastDate { get; set; }
        
        public bool IsCanGetCompanyInfo { get; set; }

        public bool IsCanCreatePayment { get; set; }

        public bool IsCanGetMovement { get; set; }

        public bool IsCanManageRegistry { get; set; }

        public bool IsCanGetAccounts { get; set; }
    }
}
