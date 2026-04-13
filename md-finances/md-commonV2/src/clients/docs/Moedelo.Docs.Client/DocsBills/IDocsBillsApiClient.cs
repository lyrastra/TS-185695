using Moedelo.Docs.Dto.Docs;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Docs.Client.DocsBills
{
    public interface IDocsBillsApiClient : IDI
    {
        /// <summary>
        /// Возвращает preview счета в формате pdf
        /// </summary>
        /// <param name="firmId"></param>
        /// <param name="userId"></param>
        /// <param name="id">documentBaseId в Bill</param>
        /// <param name="useStampAndSign"></param>
        /// <returns></returns>
        Task<HttpFileModel> DownloadPdfFileAsync(int firmId, int userId, long id, bool useStampAndSign, bool useWatermark);

        /// <summary>
        /// Возвращает preview счета в формате doc
        /// </summary>
        /// <param name="firmId"></param>
        /// <param name="userId"></param>
        /// <param name="id">documentBaseId в Bill</param>
        /// <param name="useStampAndSign"></param>
        /// <returns></returns>
        Task<HttpFileModel> DownloadDocFileAsync(int firmId, int userId, long id, bool useStampAndSign, bool useWatermark);

        /// <summary>
        /// Возвращает привязаные документы к счету
        /// </summary>
        /// <param name="firmId"></param>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<List<LinkedDocumentDto>> GetLinkedDocumentsAsync(int firmId, int userId, long id);
    }
}