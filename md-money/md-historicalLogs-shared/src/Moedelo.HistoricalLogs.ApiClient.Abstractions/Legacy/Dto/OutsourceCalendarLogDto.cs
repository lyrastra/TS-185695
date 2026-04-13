using System;
using Moedelo.HistoricalLogs.Enums;

namespace Moedelo.HistoricalLogs.ApiClient.Abstractions.Legacy.Dto
{
    public class OutsourceCalendarLogDto
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int UserId { get; set; }

        public int? ObjectId { get; set; }

        public OutsourceCalendarLogActionType ActionType { get; set; }

        public string ActionData { get; set; }
    }
}
