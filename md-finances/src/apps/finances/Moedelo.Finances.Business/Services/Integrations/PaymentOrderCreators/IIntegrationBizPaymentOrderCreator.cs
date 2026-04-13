using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.Finances.Business.Services.Integrations.PaymentOrderCreators
{
    public interface IIntegrationBizPaymentOrderCreator : IIntegrationPaymentOrderCreator, IDI
    {
    }
}
