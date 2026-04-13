using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders
{
    public interface INumberService
    {
        Task<string> GetNextNumberAsync();
    }
}