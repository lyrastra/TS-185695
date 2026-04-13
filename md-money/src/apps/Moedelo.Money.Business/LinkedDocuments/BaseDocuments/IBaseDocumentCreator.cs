using System.Threading.Tasks;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Business.LinkedDocuments.BaseDocuments
{
    internal interface IBaseDocumentCreator
    {
        Task<long> CreateForPaymentOrderAsync(BaseDocumentCreateRequest request);
        Task<long> CreateForOutgoingCashOrderAsync(BaseDocumentCreateRequest request);
        Task<long> CreateForUnifiedBudgetaryPaymentAsync(BaseDocumentCreateRequest request);
    }
}