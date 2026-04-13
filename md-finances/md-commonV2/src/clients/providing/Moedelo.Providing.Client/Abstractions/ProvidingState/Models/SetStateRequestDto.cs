using Moedelo.Providing.Client.Enums;

namespace Moedelo.Providing.Client.Abstractions.ProvidingState.Models
{
    public class SetStateRequestDto
    {
        /// <summary>
        /// Базовый идентификатор документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Тип проведения
        /// All = 0 - все за раз
        /// AccPostings = 1 - Бухгалтерские проводки
        /// TaxPostings = 2 - Налоговые проводки
        /// CustomTaxPostings - Налоговые проводки, введеные вручную
        /// </summary>
        public ProvidingStateType Type { get; set; }
    }
}
