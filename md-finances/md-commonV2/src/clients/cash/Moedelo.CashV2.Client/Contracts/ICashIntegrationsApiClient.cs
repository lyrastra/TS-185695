using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CashV2.Dto.YandexKassa.IntegrationTurn;
using Moedelo.CashV2.Dto.YandexKassa.PaymentImport;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CashV2.Client.Contracts
{
    public interface ICashIntegrationsApiClient : IDI
    {
        Task<List<MdMovementListDto>> GetIntegrations(int firmId, int userId);

        Task<bool> Skip(int firmId, int userId, int integratedFileId);

        Task<bool> Import(int firmId, int userId, int integratedFileId);
        /// <summary>
        /// Создание записи о регистрации интеграции в базе данных.
        /// </summary>
        Task<bool> IntegrationTurnAsync(int firmId, int userId, CashIntegrationTurnRequestDto dto);
    }
}
