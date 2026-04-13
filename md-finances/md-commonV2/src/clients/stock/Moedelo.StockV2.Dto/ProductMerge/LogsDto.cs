using Moedelo.Common.Enums.Enums.Stocks.ProductMerge.Logging;
using System;
using System.Collections.Generic;

namespace Moedelo.StockV2.Dto.ProductMerge
{
    public class LogsDto
    {
        public LinkedList<LogDto> Logs { get; set; } = new LinkedList<LogDto>();
    }

    public class LogDto
    {
        public LogType Type { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }
    }
}