using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.TaxReductionTip
{
    public class TaxReductionTipItemDto
    {
        /// <summary>
        /// Id контрагента с неоплаченными товарами
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Имя контрагента
        /// </summary>
        public string KontragentName { get; set; }

        /// <summary>
        /// Возможные оплаты
        /// </summary>
        public List<TaxReductionTipSubItemDto> SubItems { get; set; }
    }
}
