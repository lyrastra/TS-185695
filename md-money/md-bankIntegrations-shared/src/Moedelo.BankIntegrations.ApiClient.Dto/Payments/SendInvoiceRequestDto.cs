using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Models.PaymentOrder;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Payments
{
    public class SendInvoiceRequestDto : BaseRequestDto
    {
        public PaymentOrder PaymentOrder { get; set; }

        public string BackUrl { get; set; }
    }
}
