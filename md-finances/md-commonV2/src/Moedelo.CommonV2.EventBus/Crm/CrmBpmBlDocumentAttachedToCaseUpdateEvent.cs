namespace Moedelo.CommonV2.EventBus.Crm
{
    using System;

    /// <summary>
    ///     Событие прикрелпения документа к обновлению обращения
    /// </summary>
    public class CrmBpmBlDocumentAttachedToCaseUpdateEvent
    {
        public string DocumentId { get; set; }

        public string CaseId { get; set; }

        public string CaseUpdateId { get; set; }

        public int FirmId { get; set; }

        public int? UserId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}