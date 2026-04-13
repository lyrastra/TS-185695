using System.Threading.Tasks;

namespace Moedelo.Money.Numeration.Business.Abstractions.PaymentOrders
{
    public interface INumberSetter
    {
        Task Last(int settlementAccountId, int year, int lastNumber, bool? isSaveNumeration = null);
    }
}