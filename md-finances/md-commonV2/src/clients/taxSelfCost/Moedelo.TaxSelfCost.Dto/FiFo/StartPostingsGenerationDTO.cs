using System;

namespace Moedelo.TaxSelfCost.Dto.FiFo
{
    /// <summary>
    /// Представляет параметры, передаваемые в метод запуска генерации проводок.
    /// </summary>
    public sealed class StartPostingsGenerationDTO
    {
        /// <summary>
        /// Начало периода, за который выполняется пересчёт НУ.
        /// </summary>
        public DateTime PeriodStart { get; set; }

        /// <summary>
        /// Конец периода, за который выполняется пересчёт НУ.
        /// </summary>
        public DateTime PeriodEnd { get; set; }

        /// <summary>
        /// Год, за который будет выполняться отсылка КУДиР (если null - то отсылка КУДиР выполняться не будет).
        /// </summary>
        public int? ContinueWithSendKudirForYear { get; set; }
    }
}
