using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Mailing;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy;

public interface IMailingApiClient
{
    /// <summary>
    /// Возвращает идентификаторы всех активных рассылок, в которых есть указанный сотрудник
    /// </summary>
    Task<IReadOnlyCollection<int>> GetIdsByWorkerIdAsync(int firmId, int userId, int workerId,
        CancellationToken token = default);

    /// <summary>
    /// Добавляет в указанные рассылки сотрудника
    /// </summary>
    Task SetWorkerMailingsAsync(int firmId, int userId, SetWorkerMailingsDto dto, CancellationToken token = default);

    /// <summary>
    /// Удаляет сотрудника из всех активных рассылок
    /// </summary>
    Task DeleteFromActiveMailingByWorkerIdAsync(int firmId, int userId, int workerId, CancellationToken token);
}