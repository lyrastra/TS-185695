using Moedelo.Money.Domain.PaymentOrders.Duplicates;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest.Duplicates
{
    public interface IPaymentOrderDuplicateMerger
    {
        Task MergeAsync(long documentBaseId);
    }
}
