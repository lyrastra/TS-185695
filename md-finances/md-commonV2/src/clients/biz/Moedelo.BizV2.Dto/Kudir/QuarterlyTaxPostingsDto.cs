using System.Collections.Generic;

namespace Moedelo.BizV2.Dto.Kudir
{
    public class QuarterlyTaxPostingsDto
    {
        /// <summary>
        /// Доходы по-квартально согласно индексу: 0 - первый квартал, 1 - второй и т.д.
        /// </summary>
        public List<decimal> Gains { get; set; }

        /// <summary>
        /// Расходы по-квартально согласно индексу: 0 - первый квартал, 1 - второй и т.д.
        /// Пустой массив, если расходы не учитываются
        /// </summary>
        public List<decimal> Losses { get; set; }

        /// <summary>
        /// Обоснования для расходов
        /// </summary>
        public LossReasonPostingDto[] LossReasons { get; set; }
    }
}
