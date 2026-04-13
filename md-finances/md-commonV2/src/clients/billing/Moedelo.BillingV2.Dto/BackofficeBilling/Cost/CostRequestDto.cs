using System.Collections.Generic;

namespace Moedelo.BillingV2.Dto.BackofficeBilling.Cost
{
    public class CostRequestDto
    {
        public int FirmId { get; set; }

        public string ProductConfigurationCode { get; set; }

        public int Duration { get; set; }

        public IReadOnlyDictionary<string, object> ModifiersValues { get; set; }
    }
}