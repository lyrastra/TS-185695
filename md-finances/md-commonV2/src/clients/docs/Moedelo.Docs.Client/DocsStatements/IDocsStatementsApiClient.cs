using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Docs.Client.DocsStatements
{
    public interface IDocsStatementsApiClient : IDI
    {
        /// <summary>
        /// Возвращает preview акта в формате pdf
        /// </summary>
        /// <param name="baseId">documentBaseId в Statement</param>
        /// <returns></returns>
        Task<HttpFileModel> GetPdfPrintFileAsync(int firmId, int userId, long baseId, bool useStampAndSign);

        /// <summary>
        /// Возвращает preview акта в формате doc
        /// </summary>
        /// <param name="baseId">documentBaseId в Statement</param>
        /// <returns></returns>
        Task<HttpFileModel> GetDocPrintFileAsync(int firmId, int userId, long baseId, bool useStampAndSign);
    }
}