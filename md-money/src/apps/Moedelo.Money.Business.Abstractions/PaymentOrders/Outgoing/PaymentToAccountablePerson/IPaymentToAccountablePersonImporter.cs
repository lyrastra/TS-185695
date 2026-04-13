using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    public interface IPaymentToAccountablePersonImporter
    {
        Task ImportAsync(PaymentToAccountablePersonImportRequest request);
    }
}