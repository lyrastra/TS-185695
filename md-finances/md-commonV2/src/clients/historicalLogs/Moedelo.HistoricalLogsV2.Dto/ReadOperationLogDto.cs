using System;
using Moedelo.Common.Enums.Enums.HistoricalLogs;

namespace Moedelo.HistoricalLogsV2.Dto
{
    public class ReadOperationLogDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LogObjectType Type { get; set; }
        public int? ObjectId { get; set; }

        public int PageCount { get; set; } = 1;

        public int PageSize { get; set; } = 5000;
    }
}
