using System;
using Moedelo.Common.Enums.Enums.Nds;

namespace Moedelo.AccountingV2.Dto.Money
{
    public class MoneyTransferOperationNds
    {
        public bool IsNds { get; set; }
        public decimal NdsSum { get; set; }
        public NdsTypes NdsType { get; set; }
        public int FirmId { get; set; }
    }
}
