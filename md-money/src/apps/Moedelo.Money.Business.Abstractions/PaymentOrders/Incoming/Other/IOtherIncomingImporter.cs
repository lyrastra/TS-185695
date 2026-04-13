using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.Other
{
    public interface IOtherIncomingImporter
    {
        Task ImportAsync(ImportOtherIncomingRequest request);
    }
}
