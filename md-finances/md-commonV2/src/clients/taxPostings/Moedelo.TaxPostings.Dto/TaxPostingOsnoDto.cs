using System;
using Moedelo.Common.Enums.Enums.TaxPostings;

namespace Moedelo.TaxPostings.Dto
{
    public class TaxPostingOsnoDto
    {
        /// <summary>
        /// Идентификатор проводки
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// BaseId документа, по которому создана проводка
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// Дата проводки
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Смма проводки
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Направление движения денег (приход/расход)
        /// </summary>
        public TaxPostingsDirection Direction { get; set; }

        /// <summary>
        /// Источник дохода/расхода (только для ОСНО)
        /// </summary>
        public OsnoTransferType Type { get; set; }

        /// <summary>
        /// Вид дохода/расхода (только для ОСНО)
        /// </summary>
        public OsnoTransferKind Kind { get; set; }

        /// <summary>
        /// Тип нормируемого расхода (только для ОСНО)
        /// </summary>
        public NormalizedCostType NormalizedCostType { get; set; }

        /// <summary>
        /// Проводка создана по себестоимости для исходящей накладной
        /// </summary>
        public bool IsWaybillSelfCost { get; set; }

        /// <summary>
        /// Принятый нормированный расход за 1-й квартал
        /// </summary>
        public decimal? NormalizedFirstQuarter { get; set; }

        /// <summary>
        /// Принятый нормированный расход за 2-й квартал
        /// </summary>
        public decimal? NormalizedSecondQuarter { get; set; }

        /// <summary>
        /// Принятый нормированный расход за 3-й квартал
        /// </summary>
        public decimal? NormalizedThirdQuarter { get; set; }

        /// <summary>
        /// Принятый нормированный расход за 4-й квартал
        /// </summary>
        public decimal? NormalizedForthQuarter { get; set; }
    }
}
