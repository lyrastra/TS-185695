using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Dto.PaymentOrders;
using Moedelo.Money.Dto.PaymentOrders.Outgoing.PaymentToSupplier;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.PaymentOrders.Outgoing.PaymentToSupplier
{
    public interface IPaymentToSupplierClient : IDI
    {
        Task<PaymentOrderSaveResponseDto> CreateAsync(int firmId, int userId, PaymentToSupplierSaveDto dto);
    }
}
