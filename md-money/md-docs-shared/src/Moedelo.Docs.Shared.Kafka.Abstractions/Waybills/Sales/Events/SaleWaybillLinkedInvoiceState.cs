using Moedelo.Docs.Enums;

namespace Moedelo.Docs.Shared.Kafka.Abstractions.Waybills.Sales.Events
{
    public class SaleWaybillLinkedInvoiceState
    {
        /// <summary>
        /// Базовый идентификатор
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Флаг "НДС"
        /// </summary>
        public bool UseNds { get; set; }

        /// <summary>
        /// Тип начисления НДС (сверху/в т.ч.)
        /// </summary>
        public NdsPositionType NdsPositionType { get; set; }

        /// <summary>
        /// Счет-фактура
        /// </summary>
        public SaleWaybillNewState.LinkedInvoice Invoice { get; set; }
    }
}