using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Dto.PaymentOrders;
using Moedelo.Money.Dto.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    public interface IPaymentToAccountablePersonClient : IDI
    {
        Task<PaymentOrderSaveResponseDto> CreateAsync(int firmId, int userId, PaymentToAccountablePersonSaveDto dto);
    }
}
