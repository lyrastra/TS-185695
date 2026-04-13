using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders
{
    public interface IPaymentOrderReader<TReadResponse>
    {
        Task<TReadResponse> GetByBaseIdAsync(long documentBaseId);
    }
}
