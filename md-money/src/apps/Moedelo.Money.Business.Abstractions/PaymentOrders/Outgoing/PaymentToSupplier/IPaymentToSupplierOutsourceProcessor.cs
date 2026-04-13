using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;

public interface IPaymentToSupplierOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(PaymentToSupplierSaveRequest request);
}