using Moedelo.Money.Domain.PaymentOrders.Outgoing.AgencyContract;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.AgencyContract
{
    public interface IAgencyContractValidator
    {
        Task ValidateAsync(AgencyContractSaveRequest request);
    }
}