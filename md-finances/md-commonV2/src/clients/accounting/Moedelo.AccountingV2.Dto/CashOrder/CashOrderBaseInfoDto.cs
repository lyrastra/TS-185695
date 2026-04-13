using Moedelo.Common.Enums.Enums.PostingEngine;
using System;

namespace Moedelo.AccountingV2.Dto.CashOrder
{
    public class CashOrderBaseInfoDto
    {
        public long Id { get; set; }
        public long DocumentBaseId { get; set; }
        public OperationType OperationType { get; set; }
        public decimal Sum { get; set; }
        public long CashierId { get; set; }
        public long KontragentId { get; set; }
    }
}