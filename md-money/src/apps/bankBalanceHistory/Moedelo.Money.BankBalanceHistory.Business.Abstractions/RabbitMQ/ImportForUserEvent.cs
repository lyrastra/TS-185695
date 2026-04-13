using System;
using System.Collections.Generic;
using System.Text;

namespace Moedelo.Money.BankBalanceHistory.Business.Abstractions.RabbitMQ
{
    public class ImportForUserEvent
    {
        public int FirmId { get; set; }
        public int UserId { get; set; }
        public string FileName { get; set; }
        public string MongoObjectId { get; set; }
    }
}
