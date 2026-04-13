using System;
using Moedelo.Common.Enums.Enums.HistoricalLogs.ClosedPeriodLog;

namespace Moedelo.HistoricalLogsV2.Dto.ClosedPeriodLog
{
    public class ClosedPeriodLogGetDto
    {

        public int FirmId { get; set; }

        public int Limit { get; set; } = 20;

        public int Offset { get; set; } = 0;

    }
}
