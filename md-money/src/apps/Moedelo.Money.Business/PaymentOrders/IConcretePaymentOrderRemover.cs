using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders
{
    [MultipleImplementationsPossible]
    internal interface IConcretePaymentOrderRemover
    {
        Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null);
    }
}
