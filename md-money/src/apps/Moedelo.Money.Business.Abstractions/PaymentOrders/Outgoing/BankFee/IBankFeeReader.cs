using Moedelo.Money.Domain.PaymentOrders.Outgoing.BankFee;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BankFee
{
    public interface IBankFeeReader
    {
        Task<BankFeeResponse> GetByBaseIdAsync(long baseId);
    }
}
