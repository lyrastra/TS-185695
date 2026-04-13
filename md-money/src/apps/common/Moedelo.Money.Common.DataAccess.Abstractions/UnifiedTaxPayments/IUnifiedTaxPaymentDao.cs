using Moedelo.Money.Common.Domain.Models;
using Moedelo.Money.Enums;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Common.DataAccess.Abstractions.UnifiedTaxPayments
{
    public interface IUnifiedTaxPaymentDao
    {
        /// <summary>
        /// Возвращает дочерний ЕНП платеж по идентификатору
        /// </summary>
        Task<UnifiedBudgetarySubPayment> GetByBaseIdAsync(int firmId, long documentBaseId);

        /// <summary>
        /// Возвращает дочерниие ЕНП платежи по списку идентификаторов
        /// </summary>
        Task<IReadOnlyList<UnifiedBudgetarySubPayment>> GetByBaseIdsAsync(int firmId, IReadOnlyCollection<long> documentBaseIds, CancellationToken cancellationToken);

        /// <summary>
        /// Возвращает дочерние ЕНП платежи по идентификатору родительского платежа
        /// </summary>
        Task<IReadOnlyList<UnifiedBudgetarySubPayment>> GetByParentBaseIdAsync(int firmId, long parentBaseId);

        /// <summary>
        /// Возвращает дочерние ЕНП платежи по идентификаторам родительских платежей
        /// </summary>
        Task<IReadOnlyList<UnifiedBudgetarySubPayment>> GetByParentBaseIdsAsync(
            int firmId, IReadOnlyCollection<long> parentsBaseIds);

        /// <summary>
        /// Добавляет дочерние ЕНП платежи
        /// </summary>
        Task InsertAsync(int firmId, long documentBaseId, IReadOnlyCollection<UnifiedBudgetarySubPayment> payments);

        /// <summary>
        /// Перезаписывает дочерние ЕНП платежи
        /// </summary>
        /// <returns>Список идентификаторов удаленных платежей</returns>
        Task<IReadOnlyList<long>> OverwriteAsync(int firmId, long parentBaseId, IReadOnlyCollection<UnifiedBudgetarySubPayment> payments);

        /// <summary>
        /// Удаляет все дочерние ЕНП платежи
        /// </summary>
        /// <returns>Список идентификаторов удаленных платежей</returns>
        Task<IReadOnlyList<long>> DeleteAsync(int firmId, long parentBaseId);

        /// <summary>
        /// Обновляет статус НУ для дочернего платежа
        /// </summary>
        Task UpdateTaxPostingTypeAsync(int firmId, long documentBaseId, ProvidePostingType taxPostingType);
    }
}
