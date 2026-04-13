using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Client.PaymentOrderNumeration
{
    public interface IPaymentOrderNumerationApiClient
    {
        Task<int> GetNextNumberAsync(int firmId, int userId, int settlementAccountId, int year);

        Task<IReadOnlyCollection<int>> GetNextNumbersAsync(int firmId, int userId, int settlementAccountId, int year, int? count);
    }
}
