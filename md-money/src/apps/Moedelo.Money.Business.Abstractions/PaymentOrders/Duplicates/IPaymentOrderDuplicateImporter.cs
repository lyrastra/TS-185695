using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest.Duplicates
{
    public interface IPaymentOrderDuplicateImporter
    {
        Task ImportAsync(long documentBaseId);
    }
}
