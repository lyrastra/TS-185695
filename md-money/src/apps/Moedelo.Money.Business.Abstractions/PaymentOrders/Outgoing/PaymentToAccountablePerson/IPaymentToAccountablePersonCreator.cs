using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    public interface IPaymentToAccountablePersonCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(PaymentToAccountablePersonSaveRequest saveRequest);

        Task<PaymentOrderSaveResponse> CreateWithMissingEmployeeAsync(PaymentToAccountablePersonWithMissingEmployeeSaveRequest employeeSaveRequest);
    }
}