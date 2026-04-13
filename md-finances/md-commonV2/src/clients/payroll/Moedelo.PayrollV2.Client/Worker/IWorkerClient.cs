using System.Collections.Generic;
using System.Threading;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.PayrollV2.Dto.Worker;
using System.Threading.Tasks;

namespace Moedelo.PayrollV2.Client.Worker
{
    public interface IWorkerClient : IDI
    {
        Task<int> CreateAsync(CreateWorkerDto model, int firmId, int userId);

        /// <summary>
        /// Получить максимальный <see cref="WorkerDto.TableNumber"/>, присутствующий у работников указанной фирмы
        /// </summary>
        /// <param name="firmId">идентификатор фирмы</param>
        /// <param name="userId"></param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns>максимальное значение (0 - если нет занятых номеров)</returns>
        Task<int> GetWorkersMaxTableNumberAsync(int firmId, int userId, CancellationToken cancellationToken);

        Task UpdateAsync(UpdateWorkerDto model, int firmId, int userId);
        
        Task<List<FirmWorkersCountDto>> GetWorkersCountForLimitsAsync(IReadOnlyCollection<int> firmIds);

        Task<List<WorkPeriodsDto>> GetWorkPeriodsAsync(IReadOnlyCollection<int> workerIds, int firmId, int userId);

        Task<WorkerDto> GetWorkerAsync(int firmId, int userId, int workerId, CancellationToken cancellationToken);
    }
}