using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.TestData.Client.Dto;
using System.Threading.Tasks;

namespace Moedelo.TestData.Client
{
    public interface ITestDataApiClient : IDI
    {
        Task<TestDataSettingDto> GetAsync(int userId);

        Task<TestDataSettingDto> InitializeAsync(int firmId, int userId);
    }
}