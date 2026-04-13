using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.Docs;
using Moedelo.Docs.Dto.DocsWaybills;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Docs.Client.DocsWaybills
{
    public interface IDocsWaybillsApiClient : IDI
    {
        /// <summary>
        /// Возвращает preview счета в формате pdf (только учетка, для биза может работать некорректно)
        /// </summary>
        Task<HttpFileModel> DownloadPdfFileAsync(int firmId, int userId, WaybillFileRequestDto requestDto);

        /// <summary>
        /// Возвращает preview счета в формате doc (только учетка, для биза может работать некорректно)
        /// </summary>
        Task<HttpFileModel> DownloadDocFileAsync(int firmId, int userId, WaybillFileRequestDto requestDto);
        
        /// <summary>
        /// Возвращает привязаные документы к накладной
        /// </summary>
        /// <param name="firmId"></param>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IReadOnlyList<LinkedDocumentDto>> GetLinkedDocumentsAsync(int firmId, int userId, long id);
    }
}