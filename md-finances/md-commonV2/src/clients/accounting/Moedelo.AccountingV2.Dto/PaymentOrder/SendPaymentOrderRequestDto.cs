using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;

namespace Moedelo.AccountingV2.Dto.PaymentOrder
{
    public class SendPaymentOrderRequestDto
    {
        public int FirmId { get; set; }

        public int UserId { get; set; }

        public int PaymentOrderId { get; set; }

        public IntegrationPartners IntegrationPartner { get; set; }
    }
}