using Moedelo.Money.Domain.PaymentOrders.Outgoing.AgencyContract;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.AgencyContract
{
    public interface IAgencyContractReader : IPaymentOrderReader<AgencyContractResponse>
    {
    }
}
