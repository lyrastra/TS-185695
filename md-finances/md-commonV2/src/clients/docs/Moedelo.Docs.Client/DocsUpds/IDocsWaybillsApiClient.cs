using System.Threading;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.DocsUpds;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Docs.Client.DocsUpds
{
    /// <remarks>
    /// Только УС. БИЗ не поддерживается и никогда не предоставлял документ УПД.
    /// </remarks>
    public interface IDocsUpdsApiClient : IDI
    {
        /// <summary>
        /// Возвращает preview счета в формате pdf
        /// </summary>
        Task<HttpFileModel> DownloadPdfFileAsync(int firmId, int userId, UpdFileRequestDto requestDto, HttpQuerySetting setting = null, CancellationToken ct = default);

        /// <summary>
        /// Возвращает preview счета в формате doc
        /// </summary>
        Task<HttpFileModel> DownloadDocFileAsync(int firmId, int userId, UpdFileRequestDto requestDto, HttpQuerySetting setting = null, CancellationToken ct = default);
    }
}