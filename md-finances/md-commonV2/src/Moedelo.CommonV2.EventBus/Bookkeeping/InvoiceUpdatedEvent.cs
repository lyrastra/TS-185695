using Moedelo.Common.Enums.Enums.Documents;
using System;
using SignStatus = Moedelo.Common.Enums.Enums.Documents.SignStatus;

namespace Moedelo.CommonV2.EventBus.Bookkeeping
{
    public class InvoiceUpdatedEvent
    {
        public int FirmId { get; set; }

        public int UserId { get; set; }

        public long DocumentBaseId { get; set; }

        public PrimaryDocumentsTransferDirection Direction { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public int KontragentId { get; set; }

        /// <summary>
        /// Статус подписи. (Не подписан, Скан, Подписан)
        /// </summary>
        public SignStatus SignStatus { get; set; }

        /// <summary>
        /// Проведён в БУ
        /// </summary>
        public bool ProvideInAccounting { get; set; }

        /// <summary>
        /// Откуда создан документ
        /// </summary>
        public CreationPlaceEventEnum CreationPlace { get; set; }
    }
}