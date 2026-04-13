using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Dto.PaymentOrders;
using Moedelo.Money.Dto.PaymentOrders.Incoming.RefundFromAccountablePerson;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    public interface IRefundFromAccountablePersonClient : IDI
    {
        Task<PaymentOrderSaveResponseDto> CreateAsync(int firmId, int userId, RefundFromAccountablePersonSaveDto dto);
    }
}
