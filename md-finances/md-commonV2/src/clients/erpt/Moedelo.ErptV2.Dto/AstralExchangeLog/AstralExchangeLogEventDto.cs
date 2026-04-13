using Moedelo.Common.Enums.Enums.ElectronicReports;
using System;

namespace Moedelo.ErptV2.Dto.AstralExchangeLog
{
    public class AstralExchangeLogEventDto
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public DateTime CreateDate { get; set; }
        public AstralExchangeEventType EventType { get; set; }
        public string EventData { get; set; }
    }
}
