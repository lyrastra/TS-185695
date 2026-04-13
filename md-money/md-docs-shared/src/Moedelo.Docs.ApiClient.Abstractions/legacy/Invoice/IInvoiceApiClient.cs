using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Invoice
{
    public interface IInvoiceApiClient
    {
        Task ProvideAsync(int firmId, int userId, IReadOnlyCollection<long> invoiceBaseIds);
    }
}