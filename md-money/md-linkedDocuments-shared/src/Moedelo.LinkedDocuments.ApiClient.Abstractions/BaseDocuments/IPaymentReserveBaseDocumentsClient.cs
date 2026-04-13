using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using System.Threading.Tasks;

namespace Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments
{
    public interface IPaymentReserveBaseDocumentsClient
    {
        Task<BaseDocumentDto> GetOrCreateAsync();
    }
}