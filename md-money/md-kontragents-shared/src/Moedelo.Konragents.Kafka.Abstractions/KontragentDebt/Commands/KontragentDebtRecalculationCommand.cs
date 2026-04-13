using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Konragents.Kafka.Abstractions.KontragentDebt.Commands
{
    /// <summary>
    /// Команда на пересчёт задолженности по выбранным контрагентам одной фирмы
    /// </summary>
    public class KontragentDebtRecalculationCommand : IEntityCommandData
    {
        /// <summary>
        /// Идентификаторы контрагентов, по которым требуется пересчёт задолженности
        /// Чтобы команда успевала отрабатывать до таймаута,
        /// лучше ограничивать количество идентификаторов в 100-200 шт. за раз
        /// </summary>
        public int[] KontragentIds { get; set; }
    }
}