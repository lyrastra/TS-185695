using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.KontragentsV2.Dto.CommissionAgents;

namespace Moedelo.KontragentsV2.Client.Kontragents
{
    /// <summary>
    /// Клиент для работы с комиссионерами-контрагентами
    /// </summary>
    public interface ICommissionAgentsApiClient
    {
        /// <summary>
        /// Создает контрагента по ИНН и помечает его комиссионером.
        /// </summary>
        /// <remarks>
        /// Ошибка в случаях:
        /// - нет прав на функционал маркетплейсов
        /// - контрагент по ИНН уже существует
        /// </remarks>
        Task<CreateCommissionByInnResultDto> CreateByInnAsync(int firmId, int userId, string inn);
    }
}