using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.TaxSelfCost.Dto.FiFo;
using Moedelo.TaxSelfCost.Dto.FiFo.Enums;
using System.Threading.Tasks;

namespace Moedelo.TaxSelfCost.Client.Fifo
{
    /// <summary>
    /// Представляет сервис генерации событий запуска генерации проводок.
    /// </summary>
    public interface IPostingsGenerationProcessClient : IDI
    {
        /// <summary>
        /// Запустить генерацию проводок.
        /// </summary>
        Task StartAsync(int firmId, int userId, StartPostingsGenerationDTO parameters);

        /// <summary>
        /// Проверить статус генерации проводок.
        /// </summary>
        Task<GenerationStatusEnum> GetStatusAsync(int firmId, int userId);

        /// <summary>
        /// Проверить, активно-ли ФИФО для данной фирмы в данном году.
        /// </summary>
        Task<bool> IsFifoActiveByYearAsync(int firmId, int userId, int year);
    }
}
