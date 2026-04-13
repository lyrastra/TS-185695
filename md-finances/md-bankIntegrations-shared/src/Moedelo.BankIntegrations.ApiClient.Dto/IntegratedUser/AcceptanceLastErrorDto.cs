using System;

namespace Moedelo.BankIntegrations.ApiClient.Dto.IntegratedUser
{
    public class AcceptanceLastErrorDto
    {
        public int FirmId { get; set; }

        public int IntegrationPartner { get; set; }

        public DateTime? AcceptanceLastDate { get; set; }

        public Guid? LastErrorPaymentGuid { get; set; }

        public int ErrorPaymentCount { get; set; }
    }
}
