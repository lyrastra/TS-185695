using Moedelo.Requisites.Enums.TaxationSystems;

namespace Moedelo.Money.Providing.Business.TaxationSystems
{
    public class TaxationSystem
    {
        public int Id { get; set; }

        public short StartYear { get; set; }

        public short? EndYear { get; set; }

        public bool IsUsn { get; set; }

        public bool IsUsnProfitAndOutgo { get; set; }

        public double? UsnSize { get; set; }

        public bool IsEnvd { get; set; }

        public bool IsOsno { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }

        public TaxationSystemType DefaultTaxationSystemType { get; set; }
    }
}
