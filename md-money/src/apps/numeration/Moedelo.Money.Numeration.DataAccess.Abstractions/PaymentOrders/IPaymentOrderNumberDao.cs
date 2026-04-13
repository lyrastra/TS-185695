using System.Threading.Tasks;

namespace Moedelo.Money.Numeration.DataAccess.Abstractions.PaymentOrders
{
    public interface IPaymentOrderNumberDao
    {
        Task<int> GetLast(int firmId, int settlementAccountId, int year);
        Task SetLast(int firmId, int settlementAccountId, int year, int number);
    }
}