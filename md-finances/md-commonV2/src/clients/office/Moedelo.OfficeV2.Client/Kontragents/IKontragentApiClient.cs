using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.OfficeV2.Dto.File;

namespace Moedelo.OfficeV2.Client.Kontragents
{
    public interface IKontragentApiClient : IDI
    {
        Task<FileResponseDto> GetExcerptAsync(string innOrOgrn, HttpQuerySetting setting = null);

        Task<bool> IsExistAsync(string innOrOgrn);
    }
}