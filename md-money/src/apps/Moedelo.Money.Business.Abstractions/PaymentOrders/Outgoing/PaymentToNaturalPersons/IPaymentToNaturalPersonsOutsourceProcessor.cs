using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;

public interface IPaymentToNaturalPersonsOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(PaymentToNaturalPersonsSaveRequest request);
}