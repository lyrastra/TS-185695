using Moedelo.Money.Enums;
using System;
using TaxationSystemType = Moedelo.Money.Enums.TaxationSystemType;

namespace Moedelo.Money.Import.Domain.Models.PurseOperation
{
    public class PurseOperationFromExcel
    {
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public string SettlementAccount { get; set; }
        public PurseOperationType PurseOperationType { get; set; }
        public TaxationSystemType? TaxationSystemType { get; set; }
        public bool IncludeNds { get; set; }
        public NdsType? NdsType { get; set; }
        public decimal? NdsSum { get; set; }
    }    
}
