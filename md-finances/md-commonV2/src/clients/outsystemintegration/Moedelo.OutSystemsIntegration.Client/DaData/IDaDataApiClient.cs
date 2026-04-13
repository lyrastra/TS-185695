using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.OutSystemsIntegrationV2.Dto.DaData.Banks;

namespace Moedelo.OutSystemsIntegrationV2.Client.DaData
{
    public interface IDaDataApiClient : IDI
    {
        Task<BankResponseDto> GetBankByBik(string bik);
    }
}