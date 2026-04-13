using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;

public interface ICurrencyPaymentToSupplierOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(CurrencyPaymentToSupplierSaveRequest request);
}