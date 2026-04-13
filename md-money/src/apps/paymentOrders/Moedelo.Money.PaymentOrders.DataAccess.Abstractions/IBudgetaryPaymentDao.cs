using System.Threading.Tasks;

namespace Moedelo.Money.PaymentOrders.DataAccess.Abstractions
{
    public interface IBudgetaryPaymentDao
    {
        Task<string> GetPayerKppAsync(int firmId, long documentBaseId);
        Task SetPayerKppAsync(int firmId, long documentBaseId, string kpp);
    }
}
