using Moedelo.Common.Enums.Enums.TradingObjects;

namespace Moedelo.RequisitesV2.Dto.TradingObjects
{
    public class TradingTaxCalculationV2Dto
    {
        public TradingObjectType TradingObjectType { get; set; }

        public decimal Square { get; set; }

        public string Oktmo { get; set; }
        
        public TradingObjectClass? TradingObjectClass { get; set; }
    }
}