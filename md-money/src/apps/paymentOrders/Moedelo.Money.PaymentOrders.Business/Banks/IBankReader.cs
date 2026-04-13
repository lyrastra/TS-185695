using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.Business.Banks
{
    internal interface IBankReader
    {
        Task<Bank> GetByIdAsync(int id);

        Task<Bank> GetByBikAsync(string bik);
    }
}
