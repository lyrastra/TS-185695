using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.Finances.Domain.Models.Integrations;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Domain.Interfaces.Business.Integrations
{
    public interface IIntegrationPaymentOrderSender : IDI
    {
        Task<SendPaymentOrdersResponse> SendAsync(IUserContext userContext, IReadOnlyCollection<long> baseIds);

        Task<SendBankInvoiceResponse> SendBankInvoiceAsync(IUserContext userContext, SendBankInvoiceRequest request);
    }
}