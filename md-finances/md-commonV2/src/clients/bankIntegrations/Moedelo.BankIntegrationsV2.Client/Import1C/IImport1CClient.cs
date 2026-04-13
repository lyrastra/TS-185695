using System.Threading.Tasks;
using Moedelo.BankIntegrationsV2.Dto.Import1C;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.BankIntegrationsV2.Client.Import1C
{
    /// <summary> API для загрузки 1с выписки в сервис интеграций </summary>
    public interface IImport1CClient : IDI
    {
        Task<Import1CFileResponseDto> Import1CFileAsync(Import1CFileRequestDto requestDto);
    }
}