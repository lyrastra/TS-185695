using System;
using Moedelo.Common.Enums.Enums.HistoricalLogs;

namespace Moedelo.HistoricalLogsV2.Dto
{
    public class OperationDto
    {
        public int UserId { get; set; }
        public int FirmId { get; set; }
        public LogOperationType OperationType { get; set; }
        public LogObjectType ObjectType { get; set; }
        public long ObjectId { get; set; }
        public string ObjectData { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
