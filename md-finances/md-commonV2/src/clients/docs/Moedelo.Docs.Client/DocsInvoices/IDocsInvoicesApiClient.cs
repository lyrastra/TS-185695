using System.Threading.Tasks;
using Moedelo.Docs.Dto.DocsInvoices;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Docs.Client.DocsInvoices
{
    public interface IDocsInvoicesApiClient : IDI
    {
        /// <summary>
        /// Возвращает preview счета-фактуры в формате pdf (только учетка, для биза может работать некорректно)
        /// </summary>
        Task<HttpFileModel> DownloadPdfFileAsync(int firmId, int userId, InvoiceFileRequestDto requestDto);

        /// <summary>
        /// Возвращает preview счета-фактуры в формате doc (только учетка, для биза может работать некорректно)
        /// </summary>
        Task<HttpFileModel> DownloadDocFileAsync(int firmId, int userId, InvoiceFileRequestDto requestDto);
    }
}