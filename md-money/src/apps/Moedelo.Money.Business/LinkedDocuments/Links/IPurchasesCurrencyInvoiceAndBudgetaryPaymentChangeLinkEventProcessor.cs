using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.LinkedDocuments.Links
{
    public interface IPurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinkEventProcessor
    {
        Task ProcessAsync(IReadOnlyCollection<long> paymentBaseIds);
    }
}