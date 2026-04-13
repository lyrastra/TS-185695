using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BankFee;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BankFee
{
    public interface IBankFeeCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(BankFeeSaveRequest saveRequest);
    }
}