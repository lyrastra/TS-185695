using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Docs.Client.DocsMiddlemanReports
{
    public interface IDocsMiddlemanReportsApiClient : IDI
    {
        /// <summary>
        /// Возвращает файл отчета посредника в формате pdf
        /// </summary>
        /// <param name="firmId"></param>
        /// <param name="userId"></param>
        /// <param name="baseId">documentBaseId документа</param>
        Task<HttpFileModel> DownloadPdfAsync(int firmId, int userId, long baseId);

        /// <summary>
        /// Возвращает файл отчета посредника в формате doc
        /// </summary>
        /// <param name="firmId"></param>
        /// <param name="userId"></param>
        /// <param name="baseId">documentBaseId документа</param>
        Task<HttpFileModel> DownloadDocAsync(int firmId, int userId, long baseId);
    }
}