using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment;

public interface IRentPaymentImporter
{
    Task ImportAsync(RentPaymentImportRequest request);
}