using System;
using Moedelo.Common.Enums.Enums.TaxPostings;

namespace Moedelo.Docs.Dto.Upd
{
    public class TaxPostingDto
    {
        public long Id { get; set; }
        
        public decimal Sum { get; set; }
        
        public DateTime Date { get; set; }
        
        public OsnoTransferKind Kind { get; set; }
        
        public OsnoTransferType Type { get; set; }
        
        public TaxPostingsDirection Direction { get; set; }
        
        public string Description { get; set; }
        
        public long DocumentBaseId { get; set; }
        
        public NormalizedCostType NormalizedCostType { get; set; }
    }
}