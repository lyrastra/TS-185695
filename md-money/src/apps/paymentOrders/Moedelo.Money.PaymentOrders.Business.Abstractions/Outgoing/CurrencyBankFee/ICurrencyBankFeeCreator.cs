using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.PaymentOrders.Business.Abstractions.Models;
using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Abstractions.Outgoing.CurrencyBankFee
{
    public interface ICurrencyBankFeeCreator
    {
        Task<CreateResponse> CreateAsync(PaymentOrderSaveRequest request);
    }
}