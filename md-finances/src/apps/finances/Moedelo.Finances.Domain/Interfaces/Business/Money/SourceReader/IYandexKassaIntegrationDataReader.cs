using Moedelo.Finances.Domain.Models.Money.SourceReader;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using System.Threading.Tasks;

namespace Moedelo.Finances.Domain.Interfaces.Business.Money.SourceReader
{
    public interface IYandexKassaIntegrationDataReader: IDI
    {
        Task<IntegrationData> GetData(int firmId, int userId);
    }
}
