using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson;

public interface IRefundFromAccountablePersonImporter
{
    Task ImportAsync(RefundFromAccountablePersonImportRequest request);
}