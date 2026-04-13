using System;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.Payments;
using Moedelo.BankIntegrations.IntegrationPartnersInfo.Enums;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Business.Services.Integrations.PaymentOrderCreators
{
    public interface IIntegrationPaymentOrderCreator : IDI
    {
        Task<PaymentOrderDto> CreateAsync(Guid guid, object order, IntegrationPartners? integrationPartner = null);
    }
}
