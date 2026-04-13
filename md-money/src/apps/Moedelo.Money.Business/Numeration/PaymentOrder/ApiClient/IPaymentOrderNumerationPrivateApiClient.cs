using Moedelo.Money.Domain.PaymentOrderNumeration;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Numeration.PaymentOrder.ApiClient
{
    public interface IPaymentOrderNumerationPrivateApiClient
    {
        /// <summary>
        /// Установить минимальную границу для вычисления следующего номера
        /// </summary>
        Task SetLastNumberAsync(PaymentOrderNumerationData model);
    }
}
