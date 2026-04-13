using Moedelo.Requisites.Enums.TaxationSystems;

namespace Moedelo.Money.Domain.TaxationSystems
{
    public class TaxationSystem
    {
        public int Id { get; set; }

        public short StartYear { get; set; }

        public short? EndYear { get; set; }

        public bool IsUsn { get; set; }

        public UsnType UsnType { get; set; }

        public bool IsEnvd { get; set; }

        public bool IsOsno { get; set; }
    }
}
