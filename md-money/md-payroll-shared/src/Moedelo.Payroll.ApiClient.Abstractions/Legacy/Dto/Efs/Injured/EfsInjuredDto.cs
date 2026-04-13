using System.Collections.Generic;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Injured
{
    public class EfsInjuredDto
    {
        public List<EfsInjuredWorkerDto> Workers { get; set; }

        public List<EfsInjuredChargeDto> Charges { get; set; }

        public List<EfsInjuredContractDto> ContractSettings { get; set; }

        public List<EfsInjuredFundChargeDto> FundCharges { get; set; }

        public decimal InjuredTariffRate { get; set; }

        public int AverageWorkerCount { get; set; }

        public bool HasUnpaidFundPayments { get; set; }
    }
}