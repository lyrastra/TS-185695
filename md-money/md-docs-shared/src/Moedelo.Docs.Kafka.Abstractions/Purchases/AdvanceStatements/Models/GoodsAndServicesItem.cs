using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Models
{
    public class GoodsAndServicesItem
    {
        /// <summary>
        /// Тип позиции: хоз. расходы / работы и услуги / товары и материалы
        /// </summary>
        public AdvanceStatementItemDataType ExpenditureType { get; set; }

        /// <summary>
        /// Сумма (принято)
        /// </summary>
        public decimal AcceptedSum { get; set; }

        public IReadOnlyCollection<GoodsAndServicesExpenditureItem> SubItems { get; set; }
    }
}